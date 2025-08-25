using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;

        public AuthController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration config)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserCreationDto dto)
        {
            var userExists = await _userManager.FindByEmailAsync(dto.Email);
            if (userExists != null)
                return Conflict("User already exists");

            var user = new ApplicationUser{UserName = dto.Email, Email = dto.Email, EmailConfirmed = true};
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var requestedRole = dto.Role;
            if (string.IsNullOrWhiteSpace(requestedRole) || !await _roleManager.RoleExistsAsync(requestedRole))
                requestedRole = "Customer"; // default fallback

            await _userManager.AddToRoleAsync(user, requestedRole);
            return Created();
        }
    }
}