using meu.lms.business.Interfaces;
using meu.lms.business.JwtConfiguration;
using meu.lms.dto.DTOs.AppUserDTOs;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        private readonly RoleManager<AppRole> _roleManager;

        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtFactory _tokenService;
        private readonly ICourseService _courseService;
        private readonly ICoursePeopleService _coursePeopleService;
        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IJwtFactory tokenService, ICourseService courseService, ICoursePeopleService coursePeopleService, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _courseService = courseService;
            _coursePeopleService = coursePeopleService;
            _roleManager = roleManager;

        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUserDto>> Login(AppUserLoginDto loginDto)
        {

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user is null)
            {
                return Unauthorized("Kullanıcı Bulunamadı.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Kullanıcı adı veya şifre hatalı.");
            }


            var token = _tokenService.CreateJwt(user);

            await _signInManager.PasswordSignInAsync(user.UserName, loginDto.Password, true, false);

            var userRoles = await _userManager.GetRolesAsync(user);

            return new AppUserDto()
            {
                Name = user.Name,
                Email = loginDto.Email,
                Token = token,
                DisplayName = user.Name,
                Role = userRoles.First()
            };
        }


        [HttpPost("register")]
        public async Task<ActionResult<AppUserDto>> Register(AppUserRegisterDto registerDto)
        {

            var appUser = await _userManager.FindByEmailAsync(registerDto.Email);


            if (appUser != null)
            {
                return BadRequest("Bu mail adresi zaten kayıtlı!");
            }


            if (registerDto == null)
            {
                throw new NullReferenceException("Register Model geçerli değil!");
            }

            
            var user = new AppUser()
            {

                Email = registerDto.Email,
                UserName = registerDto.Email,
                Surname = registerDto.Surname,
                Name = registerDto.Name,

            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            await _userManager.AddToRoleAsync(user, registerDto.Role);

            List<int> courseIds = new List<int>();
            var courses = _courseService.GetAll();
            var newUser = await _userManager.FindByEmailAsync(registerDto.Email);

            foreach (var course in registerDto.Courses)
            {
                   courseIds.Add(courses.Single(I=>I.Name == course).Id);
            }

            foreach (var courseId in courseIds)
            {
                _coursePeopleService.Save(new CoursePeople()
                {
                    PersonId = newUser.Id,
                    CourseId = courseId
                });
            }


            if (result.Succeeded)
            {
                var token = _tokenService.CreateJwt(user);

                await _signInManager.PasswordSignInAsync(user.UserName, registerDto.Password, true, false);

                return new AppUserDto()
                {
                    Name = user.Name,
                    Email = registerDto.Email,
                    Token = token,
                    DisplayName = user.Name
                };
            }

            return BadRequest();
        }


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

            var token = _tokenService.CreateJwt(user);

             var userRoles = await _userManager.GetRolesAsync(user);

            return new AppUserDto()
            {
                Name = user.Name,
                Email = user.Email,
                DisplayName = user.Name,
                Token = token,
                Role = userRoles.First()
            };
        }



    }
}
