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

namespace meu.lms.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ArticlesController : ControllerBase
    {

        private readonly IArticleService _articleService;
        private readonly ICourseService _courseService;
        public ArticlesController(IArticleService articleService, ICourseService courseService)
        {
            _articleService = articleService;
            _courseService = courseService;
        }


        [HttpGet]
        public IActionResult GetArticles()
        {
            return Ok(_articleService.GetAll());
        }

        [HttpGet("{courseId}")]
        public IActionResult GetCourseArticles(int courseId)
        {
            return Ok(_articleService.GetAll().Where(I=>I.CourseId==courseId).ToList());
        }

        [HttpGet("{articleId}")]
        public IActionResult GetCourseArticleWithId(int articleId)
        {
            return Ok(_articleService.GetAll().Single(I => I.CourseId == articleId));
        }

        [HttpPost]
        [TypeFilter(typeof(ValidInstructorRole))]
        public IActionResult Publish(ArticleAddViewModel articleAddViewModel)
        {

            var appuserId = Convert.ToInt32(HttpContext.User?.Claims?.FirstOrDefault(I => I.Type == ClaimTypes.NameIdentifier)?.Value);


            _articleService.Save(new Article()
            {
                Text = articleAddViewModel.Text,
                CourseId = articleAddViewModel.CourseId,
                AppUserId = appuserId
            });

            return Ok();
        }
    }
}
