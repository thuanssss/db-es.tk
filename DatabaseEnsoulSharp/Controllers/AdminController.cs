using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseEnsoulSharp.Models.Database;
using DatabaseEnsoulSharp.Models.Parameter;
using DatabaseEnsoulSharp.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DatabaseEnsoulSharp.Controllers
{
    public class AdminController : BaseController
    {
        private readonly IChampionService _championService;
        private readonly IScriptInfoService _scriptInfoService;
        private readonly IChampionScriptService _championScriptService;
        public AdminController(IChampionService championService, IScriptInfoService scriptInfoService, IChampionScriptService championScriptService)
        {
            _championService = championService;
            _scriptInfoService = scriptInfoService;
            _championScriptService = championScriptService;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username)
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ChampionView()
        {
            return PartialView();
        }

        public async Task<IActionResult> TableChampion()
        {
            return PartialView(await _championService.GetAllChampion());
        }

        public async Task<IActionResult> ActionCreateChampion([FromBody] ActionCreateChampionParameter model)
        {
            return Json(await _championService.CreateChampion(model));
        }




        public IActionResult ScriptView()
        {
            return PartialView();
        }
        public async Task<IActionResult> TableScript()
        {
            return PartialView(await _scriptInfoService.GetAllScript());
        }
        public async Task<IActionResult> ActionCreateScriptInfo([FromBody] ActionCreateScriptInfoParameter model)
        {
            return Json(await _scriptInfoService.CreateScript(model));
        }


        public async Task<IActionResult> ChampionScriptView()
        {
            ViewData["scripts"] = await _scriptInfoService.GetAllScriptForDropdown();
            ViewData["champions"] = await _championService.GetAllChampionForDropdown();
            return PartialView();
        }
        public async Task<IActionResult> TableChampionScript()
        {
            return PartialView(await _championScriptService.GetAllChampionScript());
        }

        public async Task<IActionResult> ActionCreateChampionScript([FromBody] ActionCreateChampionScriptParameter model)
        {
            return Json(await _championScriptService.CreateChampionScript(model));
        }
    }
}