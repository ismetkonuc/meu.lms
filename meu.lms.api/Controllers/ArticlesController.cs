using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using meu.lms.api.CustomFilters;
using meu.lms.api.Models.ArticleModels;
using meu.lms.business.Interfaces;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace meu.lms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ArticlesController : ControllerBase
    {

        private readonly IArticleService _articleService;
        private readonly ICourseService _courseService;
        private readonly UserManager<AppUser> _userManager;
        public ArticlesController(IArticleService articleService, ICourseService courseService, UserManager<AppUser> userManager)
        {
            _articleService = articleService;
            _courseService = courseService;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult GetArticles()
        {
            return Ok(_articleService.GetAll());
        }

        [HttpGet("{courseId}")]
        public IActionResult GetCourseArticles(int courseId)
        {

            var coursePosts = _articleService.GetAll().Where(I => I.CourseId == courseId).ToList();

            List<ArticleListViewModel> articleListViewModels = new List<ArticleListViewModel>();

            ArticleListViewModel articleListViewModel = new ArticleListViewModel();

            foreach (var coursePost in coursePosts)
            {

                var appUser = _userManager.FindByIdAsync(coursePost.AppUserId.ToString()).Result;


                articleListViewModels.Add(new ArticleListViewModel()
                {
                    AppUserFullName = $"{appUser.Name} {appUser.Surname}",
                    Id = coursePost.Id,
                    Text = coursePost.Text,
                    PostedTime = coursePost.PostedTime,
                    Title = coursePost.Title
                });

            }


            return Ok(articleListViewModels.OrderByDescending(I=>I.PostedTime));
        }


        [HttpPost]
        [TypeFilter(typeof(ValidInstructorRole))]
        public IActionResult Publish([FromBody] ArticleAddViewModel articleAddViewModel)
        {

            var appuserId = Convert.ToInt32(HttpContext.User?.Claims?.FirstOrDefault(I => I.Type == ClaimTypes.NameIdentifier)?.Value);


            _articleService.Save(new Article()
            {
                Text = articleAddViewModel.Text,
                CourseId = articleAddViewModel.CourseId,
                AppUserId = appuserId,
                Title = articleAddViewModel.Title
            });

            return Ok();
        }

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {

            var article = _articleService.GetAll().Single(I => I.Id == id);

            _articleService.Delete(article);

            return Ok();
        }

    }
}
