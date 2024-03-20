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
        public async Task<ApiResponseType<AuthResponse>> Register(RegistrationRequest request)
        {
            var user = new ApplicationUser { UserName = request.Username, Email = request.Email, Role = request.role };
           // var response = new GetUserDTO { Email = user.Email, UserName = user.UserName };

            var result = await _userManager.CreateAsync(user, request.Password);

            var AccessToken = _tokenService.CreateToken(user);
            await _dataContext.SaveChangesAsync();

            AuthResponse response = new AuthResponse
            {
                Username = user.UserName,
                Email = user.Email,
                Token = AccessToken
            };


            if (result.Succeeded)
            {

                AuthRequest temp = new AuthRequest { Email = request.Email, Password = request.Password };
                await Authenticate(temp);
                return new ApiResponseType<AuthResponse>(true, "", response);
            }

            else
            {
                return new ApiResponseType<AuthResponse>(false, "Bad email or password", response);
            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<ApiResponseType<AuthResponse>> Authenticate([FromBody] AuthRequest request)
        {
            if (!ModelState.IsValid)
            {
                var resp = new AuthResponse();
                return new ApiResponseType<AuthResponse>(false, "Bad Request", resp);
            }

            var managedUser = await _userManager.FindByEmailAsync(request.Email!);

            if (managedUser == null)
            {
                var resp = new AuthResponse();
                return new ApiResponseType<AuthResponse>(false, "Bad Email", resp);
            }

            var isPassordValid = await _userManager.CheckPasswordAsync(managedUser, request.Password);

            if (!isPassordValid)
            {
                var resp = new AuthResponse();
                return new ApiResponseType<AuthResponse>(false, "Incorrect Password", resp);
            }

            var userInDb = _dataContext.Users.FirstOrDefault(u => u.Email == request.Email);

            if (userInDb == null)
            {
                var resp = new AuthResponse();
                return new ApiResponseType<AuthResponse>(false, "User does not exist", resp);
            }

            var AccessToken = _tokenService.CreateToken(userInDb);
            await _dataContext.SaveChangesAsync();

           AuthResponse user = new AuthResponse
            {
                Username = userInDb.UserName,
                Email = userInDb.Email,
                Token = AccessToken
            };


            var response = new ApiResponseType<AuthResponse>(true, "", user);
           
            return response;
        }

        [HttpPut]
        [Authorize(Roles = "User, Admin")]
        [Route("update")]
        public async Task<ApiResponseType<AuthResponse>> UpdateUser(UserUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                var resp = new AuthResponse();
                return new ApiResponseType<AuthResponse>(false, "Bad Request", resp);
            }
            var currEmail = User.Email();
            var currentUser = await _userManager.FindByEmailAsync(currEmail);



            if (!string.IsNullOrEmpty(request.NewPassword))
            {
                var result = await _userManager.ChangePasswordAsync(currentUser, request.CurrentPassword, request.NewPassword);

                if (!result.Succeeded)
                {
                    var resp = new AuthResponse();
                    return new ApiResponseType<AuthResponse>(false, "Bad Password Change Request", resp);
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


                var response = new ApiResponseType<AuthResponse>(true, "", user);
                return response;

            }
            else
            {
                var resp = new AuthResponse();
                return new ApiResponseType<AuthResponse>(false, "Bad Request", resp);
            }

        }

        [HttpGet]
        [Authorize(Roles = "User, Admin")]
        [Route("get")]
        public async Task<ApiResponseType<GetUserWInfoDTO>> getUser()
        {

            var currEmail = User.Email();
            var currentUser = await _userManager.FindByEmailAsync(currEmail);


            var user = new GetUserWInfoDTO
            {
                UserName = currentUser.UserName,
                Email = currentUser.Email
            };

            if(currentUser == null)
            {
                var ErrResponse = new ApiResponseType<GetUserWInfoDTO>(false, "No user logged in", user);
                return ErrResponse;
            }
            var response = new ApiResponseType<GetUserWInfoDTO>(true, "Success", user);
            return response;
        }



    }
}
