using Bobflix_Backend.ApiResponseType;
using Bobflix_Backend.Data.DTO;
using Bobflix_Backend.Enums;
using Bobflix_Backend.Helpers;
using Bobflix_Backend.Models;
using Bobflix_Backend.Models.Request;
using Bobflix_Backend.Models.Response;
using Bobflix_Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace Bobflix_Backend.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly DatabaseContext _dataContext;
        private readonly TokenService _tokenService;

        public UsersController(UserManager<ApplicationUser> userManager, DatabaseContext context, TokenService tokenService, ILogger<UsersController> logger)
        {
            _userManager = userManager;
            _dataContext = context;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegistrationRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser { UserName = request.Username, Email = request.Email, Role = request.role };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
               
                return CreatedAtAction(nameof(Register), new { email = request.Email, role = Role.User }, request);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
            return BadRequest(ModelState);
        }


        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var managedUser = await _userManager.FindByEmailAsync(request.Email!);

            if (managedUser == null)
            {
                return BadRequest("Bad email");
            }

            var isPassordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);

            if (!isPassordValid)
            {
                return BadRequest("Bad password");
            }

            var userInDb = _dataContext.Users.FirstOrDefault(u => u.Email == request.Email);

            if (userInDb == null)
            {
                return Unauthorized();
            }

            var AccessToken = _tokenService.CreateToken(userInDb);
            await _dataContext.SaveChangesAsync();

           AuthResponse user = new AuthResponse
            {
                Username = userInDb.UserName,
                Email = userInDb.Email,
                Token = AccessToken
            };


            var response = new ApiResponseType<AuthResponse>
            {
                Data = user,
                Success = true,
                ErrorMessage = string.Empty
            };
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "User, Admin")]
        [Route("update")]
        public async Task<IActionResult> UpdateUser(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var currEmail = User.Email();
            var currentUser = await _userManager.FindByEmailAsync(currEmail);



            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                var result = await _userManager.ChangePasswordAsync(currentUser, request.CurrentPassword, request.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }
            }


            var updatedResult = await _userManager.UpdateAsync(currentUser);

           

            if (updatedResult.Succeeded)
            {

                AuthResponse user = new AuthResponse
                {
                    Username = currentUser.UserName,
                    Email = currentUser.Email
                };


                var response = new ApiResponseType<AuthResponse>
                {
                    Data = user,
                    Success = true,
                    ErrorMessage = string.Empty
                };
                return Ok(response);

            }
            else
            {
                foreach (var error in updatedResult.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }

        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        [Route("get")]
        public async Task<IResult> getUser()
        {

            var currEmail = User.Email();
            var currentUser = await _userManager.FindByEmailAsync(currEmail);


            GetUserDTO user = new GetUserDTO
            {
                UserName = currentUser.UserName,
                Email = currentUser.Email
            };

            if(currentUser.Email == null)
            {
                var ErrResponse = new ApiResponseType<GetUserDTO> { 
                    Success = false, 
                    Data = user,
                    ErrorMessage = "User not logged in"};
                return TypedResults.Ok(ErrResponse);
            }
            var response = new ApiResponseType<GetUserDTO>
            {
                Data = user,
                Success = true,
                ErrorMessage = string.Empty
            };
            return TypedResults.Ok(response);
        }



    }
}
