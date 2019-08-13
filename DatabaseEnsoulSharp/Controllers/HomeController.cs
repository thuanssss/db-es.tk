using DatabaseEnsoulSharp.Models.Database;
using DatabaseEnsoulSharp.Models.Extensions;
using DatabaseEnsoulSharp.Models.Parameter;
using DatabaseEnsoulSharp.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatabaseEnsoulSharp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMyCache _cache;
        private readonly IChampionScriptService _championScriptService;
        private readonly ILoginService _loginService;
        private readonly IRatingService _ratingService;
        private readonly IFirebaseService _firebaseService;
        private readonly IResponseServerService _responseServerService;

        public HomeController(IMyCache cache, IChampionScriptService championScriptService,
            ILoginService loginService, IRatingService ratingService,
            IFirebaseService firebaseService,
            IResponseServerService responseServerService
            )
        {
            _cache = cache;
            _championScriptService = championScriptService;
            _loginService = loginService;
            _ratingService = ratingService;
            _firebaseService = firebaseService;
            _responseServerService = responseServerService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Jumbotron()
        {
            return PartialView();
        }

        public async Task<IActionResult> ScriptForChampion(int idChampion, string championName)
        {
            ViewBag.ChampionName = championName;
            ViewBag.IdChampion = idChampion;

            var arrayChampionScript = await _championScriptService.GetListChampionScript(idChampion);

            ViewData["scripts"] = arrayChampionScript;

            return PartialView();
        }

        [HttpPost]
        public async Task<JsonResult> Login([FromBody]LoginParameter model)
        {
            await _loginService.Login(model);

            var json = model.ToJson();

            Response.SetCookie("User", json, 30);

            return Json(true);
        }

        [HttpPost]
        public JsonResult Logout()
        {
            Response.RemoveCookie("User");

            return Json(true);
        }

        [HttpGet]
        public IActionResult ClearCache(string username, string password)
        {
            if (_loginService.ClearCache(username, password))
            {
                _cache.Clear();
                return new JsonResult(true);
            }

            return new JsonResult(false);
        }

        [HttpPost]
        public async Task<JsonResult> ActionRating([FromBody]ActionRatingParameter model)
        {
            var userString = Request.Cookies["User"];

            var userModel = userString.ToObject<User>();

            if (userModel == null) return Json(_responseServerService.ResponseError("Please login first !!!"));

            _cache.Remove($"Champion-{model.IdChampion}");

            return Json(await _ratingService.RatingScript(model, userModel));
        }

        [HttpPost]
        public async Task<JsonResult> ActionStatus([FromBody]ActionStatusParameter model)
        {
            var userString = Request.Cookies["User"];

            var userModel = userString.ToObject<User>();

            if (userModel == null) return Json(_responseServerService.ResponseError("Please login first !!!"));

            _cache.Remove($"Champion-{model.IdChampion}");

            var data = await _championScriptService.ChangeStatusScript(model);

            if (!data) return Json(_responseServerService.ResponseError());

            return Json(_responseServerService.ResponseSuccess("Change Status Success !!!"));
        }
    }
}