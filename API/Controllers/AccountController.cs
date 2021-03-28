using AutoMapper;
using GetGroup.API.Controllers;
using GetGroup.API.Dtos;
using GetGroup.API.Errors;
using GetGroup.API.Extensions;
using GetGroup.Core.Entities;
using GetGroup.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static GetGroup.Core.Helpers.Enum;

namespace GetGroup.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUnitOfWork unitOfWork,
        ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var user = await _userManager.FindByEmailFromClaimsPrinciple(User);
            var role = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                DisplayName = user.FirstName + ' ' + user.LastName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user,role.FirstOrDefault()),
            };
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }



        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null) return Unauthorized(new ApiResponse(401));

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDto
            {
                DisplayName = user.FirstName + ' ' + user.LastName,
                Email = user.Email,
                Token = _tokenService.CreateToken(user,roles.FirstOrDefault()),
                Role = roles.FirstOrDefault()
            };
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (CheckEmailExistsAsync(registerDto.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationErrorResponse { Errors = new[] { "Email address is in use" } });
            }

            var user = new AppUser
            {
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                UserName=registerDto.Email
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            
            var userService = new UserService
            {
                ServiceId = registerDto.ServiceId,
                UserId = user.Id,
                IsDeleted = false,
                RequestSatusId = RequestStatus.InProgress,
                CreateDate=DateTime.Now
                
            };
            _unitOfWork.Repository<UserService>().Add(userService);
            await _unitOfWork.Complete();

            await _userManager.AddToRoleAsync(user, "User");

            return new UserDto
            {
                DisplayName = user.FirstName + " " + user.LastName,
                Token = _tokenService.CreateToken(user,"User"),
                Email = user.Email
            };
        }
    }
}
