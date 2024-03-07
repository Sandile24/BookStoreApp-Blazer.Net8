using AutoMapper;
using BookStoreAppAPI.Data;
using BookStoreAppAPI.DTO_s.UserDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace BookStoreAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;
        public AuthenticationController( ILogger<AuthenticationController> logger, IMapper mapper, UserManager<ApiUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userDTO)
        {
            _logger.LogInformation($"Registration Attempt for {userDTO.Email}");
            try
            {
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;
                var result = await _userManager.CreateAsync(user, userDTO.Password);

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
        public async Task<IActionResult> Login(LoginUserDTO userDTO)
        {
            _logger.LogInformation($"Login Attempt for {userDTO.Email}");
            try
            {
                var user = await _userManager.FindByEmailAsync(userDTO.Email);
                if (user == null)
                    return Unauthorized("Invalid email or password");

                var passwordValid = await _userManager.CheckPasswordAsync(user, userDTO.Password);
                if (!passwordValid)
                    return Unauthorized("Invalid email or password");

                // Login successful
                // You may want to generate a token here or perform any other necessary actions

                return Ok("Login successful"); // You can return any relevant data here
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Something went wrong in the {nameof(Login)}");
                return Problem($"Something went wrong in the {nameof(Login)}", statusCode: 500);
            }
        }

        //public async Task<IActionResult> Login(LoginUserDTO userDTO)
        //{
        //    _logger.LogInformation($"Registration Attempt for {userDTO.Email}");
        //    try
        //    {
        //        var user = await _userManager.FindByEmailAsync(userDTO.Email);
        //        var passwordValid = await _userManager.CheckPasswordAsync(user!, userDTO.Password);
        //        if (user == null || passwordValid==false)
        //            return NotFound("Unable to login, Check your login details");

        //        return Accepted();
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e, $"Something went wrong in the {nameof(Register)}");
        //        return Problem($"Something went wrong in the {nameof(Register)}", statusCode: 500);
        //    }

        //}
    }
}
