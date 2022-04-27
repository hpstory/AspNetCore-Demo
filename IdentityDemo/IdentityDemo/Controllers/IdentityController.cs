using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<User> userManager;

        private readonly RoleManager<Role> roleManager;

        public IdentityController(
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        [HttpPost("role")]
        [Authorize(Roles="admin")]
        public async Task<ActionResult> AddRoleAsync([FromBody] string userName)
        {
            if (!await this.roleManager.RoleExistsAsync("admin"))
            {
                Role role = new Role
                {
                    Name = "admin"
                };

                var roleResult = await roleManager.CreateAsync(role);
                if (!roleResult.Succeeded) return BadRequest();
            }

            User user = await userManager.FindByNameAsync(userName);
            if (user == null)
            {
                user = new User
                {
                    UserName = userName
                };

                var userResult = await userManager.CreateAsync(user, "Qwer!234");
                if (!userResult.Succeeded) return BadRequest();
            }

            if (!await this.userManager.IsInRoleAsync(user, "admin"))
            {
                var userToRoleResult = await this.userManager.AddToRoleAsync(user, "admin");
                if (!userToRoleResult.Succeeded) return BadRequest();
            }
            
            return Created("/", user);
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] LoginInfo loginInfo)
        {
            string userName = loginInfo.UserName;
            string password = loginInfo.Password;
            var user = await this.userManager.FindByNameAsync(userName);
            if (user == null) return NotFound();
            if (await this.userManager.IsLockedOutAsync(user)) return BadRequest(user.LockoutEnd);
            if (!await this.userManager.CheckPasswordAsync(user, password))
            {
                await this.userManager.AccessFailedAsync(user);
                return BadRequest();
            }

            await this.userManager.ResetAccessFailedCountAsync(user);
            return Ok();
        }

        [HttpPost("login/jwt")]
        [AllowAnonymous]
        public async Task<ActionResult> LoginWithJwtAsync(
            [FromBody] LoginInfo loginInfo,
            [FromServices] IOptions<JWTOptions> jwtOptions)
        {
            var userName = loginInfo.UserName;
            var password = loginInfo.Password;
            var user = await this.userManager.FindByNameAsync(userName);
            if (user == null) return NotFound();

            if (!await this.userManager.CheckPasswordAsync(user, password)) return BadRequest();
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(ClaimTypes.Role, "admin"));
            var roles = await userManager.GetRolesAsync(user);
            foreach (string role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            string jwtToken = BuildToken(claims, jwtOptions.Value);
            return Ok(jwtToken);
        }

        [HttpGet("passwordToken")]
        // [Authorize] move to controller
        public async Task<ActionResult> GetPasswordResetTokenAsync([FromQuery] string userName)
        {
            var user = await this.userManager.FindByNameAsync(userName);
            if (user == null) return NotFound();
            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);
            return Ok(token);
        }

        [HttpGet]
        public ActionResult GetUserInfo()
        {
            string id = this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            string userName = this.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            IEnumerable<Claim> roleClaims = this.User.FindAll(ClaimTypes.Role);
            string roleNames = string.Join(',', roleClaims.Select(c => c.Value));
            return Ok($"id={id},userName={userName},roleNames={roleNames}");
        }

        [HttpPut("resetPassword")]
        [AllowAnonymous]
        public async Task<ActionResult> ResetPassword(string userName, string token, string password)
        {
            var user = await this.userManager.FindByNameAsync(userName);
            if (user == null) return NotFound();
            var result = await this.userManager.ResetPasswordAsync(user, token, password);
            if (!result.Succeeded) return BadRequest();
            return Ok();
        }

        private static string BuildToken(IEnumerable<Claim> claims, JWTOptions options)
        {
            DateTime expires = DateTime.Now.AddSeconds(options.ExpireTime);
            byte[] keyBytes = Encoding.UTF8.GetBytes(options.SigningKey);
            var secKey = new SymmetricSecurityKey(keyBytes);
            var credentials = new SigningCredentials(secKey,
                SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(expires: expires,
                signingCredentials: credentials, claims: claims);
            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
