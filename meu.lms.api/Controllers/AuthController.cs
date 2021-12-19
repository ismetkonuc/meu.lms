using meu.lms.business.Interfaces;
using meu.lms.business.JwtConfiguration;
using meu.lms.dto.DTOs.AppUserDTOs;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace meu.lms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtFactory _tokenService;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtFactory tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;

        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserDto>> Login(AppUserLoginDto loginDto)
        {

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
            {
                return Unauthorized("Kullanıcı Bulunamadı.");
            }

            //var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Kullanıcı adı veya şifre hatalı.");
            }


            //JwtFactory jwtFactory = new JwtFactory();
            //var token = jwtFactory.CreateJwt(user);
            var token = _tokenService.CreateJwt(user);

            await _signInManager.PasswordSignInAsync(user.UserName, loginDto.Password, true, false);

            return new AppUserDto()
            {
                Name = user.Name,
                Email = loginDto.Email,
                Token = token,
                DisplayName = user.Name
            };
        }


        [HttpPost("register")]
        public async Task<ActionResult<AppUserDto>> Register(AppUserRegisterDto registerDto)
        {

            if (registerDto == null)
            {
                throw new NullReferenceException("Register Model is null!");
            }

            
            var user = new AppUser()
            {

                Email = registerDto.Email,
                UserName = registerDto.UserName,
                Surname = registerDto.Surname,
                Name = registerDto.Name,

            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            await _userManager.AddToRoleAsync(user, "Student");

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest();
        }


        //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        //[Authorize]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet("currentuser")]
        public async Task<ActionResult<AppUserDto>> GetCurrentUserInfo()
        {
            //HttpContext.User.Identity.

            //var tokenHandler = new JwtSecurityTokenHandler();
            //var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            //var claims = securityToken.Claims;


            var userId = HttpContext.User?.Claims?.FirstOrDefault(I => I.Type == ClaimTypes.NameIdentifier)?.Value;

            //var user = _appUserService.GetCurrentUser(email);
            var user = await _userManager.FindByIdAsync(userId);

            return new AppUserDto()
            {
                Name = user.Name,
                Email = user.Email,
                DisplayName = user.Name
            };
        }



    }
}
