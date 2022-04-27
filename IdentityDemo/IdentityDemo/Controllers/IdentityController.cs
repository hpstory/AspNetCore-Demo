using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IdentityDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("passwordToken")]
        public async Task<ActionResult> GetPasswordResetTokenAsync([FromQuery] string userName)
        {
            var user = await this.userManager.FindByNameAsync(userName);
            if (user == null) return NotFound();
            var token = await this.userManager.GeneratePasswordResetTokenAsync(user);
            return Ok(token);
        }

        [HttpPut("resetPassword")]
        public async Task<ActionResult> ResetPassword(string userName, string token, string password)
        {
            var user = await this.userManager.FindByNameAsync(userName);
            if (user == null) return NotFound();
            var result = await this.userManager.ResetPasswordAsync(user, token, password);
            if (!result.Succeeded) return BadRequest();
            return Ok();
        }
    }
}
