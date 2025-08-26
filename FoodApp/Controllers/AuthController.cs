using FoodApp.DTOs.Restaurants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
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
            // check if user already exists
            var userExists = await _userManager.FindByEmailAsync(dto.Email);
            if (userExists != null)
                return Conflict("User already exists");

            var user = new ApplicationUser { UserName = dto.Email, Email = dto.Email, EmailConfirmed = true };
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            var requestedRole = dto.Role;
            if (string.IsNullOrWhiteSpace(requestedRole) || !await _roleManager.RoleExistsAsync(requestedRole))
                requestedRole = "Customer"; // default fallback

            await _userManager.AddToRoleAsync(user, requestedRole);
            return StatusCode(StatusCodes.Status201Created, new { Id = user.Id, Email = user.Email!, Role = requestedRole });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequestDto dto)
        {   
            // validate credentials
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized("Invalid credentials!");

            // get user role
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.First();

            // add claims to the token
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            var response = new UserResponseDto
            {
                Id = user.Id,
                Email = user.Email!,
                Role = role,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = token.ValidTo
            };
            return Ok(response);
        }
    }
}