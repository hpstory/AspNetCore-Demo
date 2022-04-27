using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// write token
List<Claim> claims = new List<Claim>();
claims.Add(new Claim(ClaimTypes.NameIdentifier, "6"));
claims.Add(new Claim(ClaimTypes.Name, "Tom"));
claims.Add(new Claim(ClaimTypes.Role, "User"));
claims.Add(new Claim(ClaimTypes.Role, "Admin"));
claims.Add(new Claim("Account", "E90000082"));
string key = "not easy veryveryverylong";
DateTime expires = DateTime.Now.AddDays(1);
byte[] secBytes = Encoding.UTF8.GetBytes(key);
var secKey = new SymmetricSecurityKey(secBytes);
var credentials = new SigningCredentials(secKey, SecurityAlgorithms.HmacSha256Signature);
var tokenDescriptor = new JwtSecurityToken(claims: claims,
    expires: expires, signingCredentials: credentials);
string jwt = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
Console.WriteLine(jwt);

// decode
JwtSecurityTokenHandler tokenHandler = new();
TokenValidationParameters valParam = new();
var fakeKey = "try hack"; // throw exception in line 32
var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(fakeKey));
// var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
valParam.IssuerSigningKey = securityKey;
valParam.ValidateIssuer = false;
valParam.ValidateAudience = false;
ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(jwt,
		valParam, out SecurityToken secToken);
foreach (var claim in claimsPrincipal.Claims)
{
	Console.WriteLine($"{claim.Type}={claim.Value}");
}
