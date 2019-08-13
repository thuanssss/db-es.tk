using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseEnsoulSharp.Models.Extensions;
using DatabaseEnsoulSharp.Models.Parameter;
using DatabaseEnsoulSharp.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseEnsoulSharp.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }


        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ActionLogin([FromBody]ActionLoginParameter model)
        {

            if (_loginService.ClearCache(model.UserName, model.Password))
            {
                var json = model.ToJson();
                Response.SetCookie("Admin", json, 30);
                return Json(true);
            }

            return Json(false);
        }
    }
}