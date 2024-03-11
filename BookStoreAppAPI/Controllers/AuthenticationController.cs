using AutoMapper;
using BookStoreAppAPI.Data;
using BookStoreAppAPI.DTO_s.User;
using BookStoreAppAPI.DTO_s.UserDTO;
using BookStoreAppAPI.Static;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        private readonly IConfiguration _configueration;
        public AuthenticationController( ILogger<AuthenticationController> logger, IMapper mapper, UserManager<ApiUser> userManager, IConfiguration configuration)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _configueration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            _logger.LogInformation($"Registration Attempt for {userDTO.Email}");
            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user, userDTO.Password!);

                if (result.Succeeded == false)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                //assigning user to role
                await _userManager.AddToRoleAsync(user, "User");
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,$"Something went wrong in the {nameof(Register)}");
                return Problem($"Something went wrong in the {nameof(Register)}",statusCode:500);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(LoginUserDTO userDTO)
        {
            _logger.LogInformation($"Login Attempt for {userDTO.Email}");
            try
            {
                var user = await _userManager.FindByEmailAsync(userDTO.Email!);
                if (user == null)
                    return Unauthorized("Invalid email, Please input a correct email");

                var passwordValid = await _userManager.CheckPasswordAsync(user, userDTO.Password!);
                if (!passwordValid)
                    return Unauthorized("Invalid password, work around it and make it work");

                // You may want to generate a token here or perform any other necessary actions
                string tokenString = await GenerateToken(user);

                // Creating a new authentication response object with provided email, token, and user ID.
                var response = new AuthResponse
                {
                    Email = userDTO.Email!, // Assigning the email from the user DTO
                    Token = tokenString,    // Assigning the generated token string
                    UserId = user.Id,       // Assigning the ID of the user
                };

                // change the return statement and return response to get the token
                return response; // You can return any relevant data here
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }

        //After this method, create a response class
        //method should be a string since it returns a string token.
        private async Task<string> GenerateToken(ApiUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configueration["JwtSettings:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var roles = await  _userManager.GetRolesAsync(user);
            var roleClaims = roles.Select(q=> new Claim(ClaimTypes.Role, q)).ToList();

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(CustomClaimType.Uid, user.Id) //CustomClaimType is a class created to contain Uid
            }
            .Union(userClaims)
            .Union(roleClaims);

            //generate token
            var token = new JwtSecurityToken(
                issuer: _configueration["JwtSettings:Issuer"],
                audience: _configueration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(Convert.ToInt32(_configueration["JwtSettings:Duration"])),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
