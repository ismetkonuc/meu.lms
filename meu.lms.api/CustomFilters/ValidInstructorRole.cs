using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;
using meu.lms.entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace meu.lms.api.CustomFilters
{
    public class ValidInstructorRole : ActionFilterAttribute
    {
        private readonly UserManager<AppUser> _userManager;

        public ValidInstructorRole(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            var appuserId = Convert.ToInt32(context.HttpContext.User?.Claims?.FirstOrDefault(I => I.Type == ClaimTypes.NameIdentifier)?.Value);

            var user = _userManager.FindByIdAsync(appuserId.ToString());

            var userRoles = _userManager.GetRolesAsync(user.Result);

            if (userRoles.Result.Contains("Student"))
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}