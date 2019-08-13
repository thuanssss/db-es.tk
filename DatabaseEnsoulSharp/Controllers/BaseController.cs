using DatabaseEnsoulSharp.Models.Extensions;
using DatabaseEnsoulSharp.Models.Parameter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DatabaseEnsoulSharp.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                var userString = Request.Cookies["Admin"];
                var userModel = userString.ToObject<ActionLoginParameter>();

                if (string.IsNullOrWhiteSpace(userModel.UserName) || string.IsNullOrWhiteSpace(userModel.Password))
                {
                    filterContext.Result = new RedirectResult(Url.Action("Index", "Login"));
                }
            }
            catch
            {
                filterContext.Result = new RedirectResult(Url.Action("Index", "Login"));
            }

        }
    }
}