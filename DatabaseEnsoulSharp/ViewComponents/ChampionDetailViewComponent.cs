using DatabaseEnsoulSharp.Models.Database;
using DatabaseEnsoulSharp.Models.Extensions;
using DatabaseEnsoulSharp.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DatabaseEnsoulSharp.ViewComponents
{
    public class ChampionDetailViewComponent : ViewComponent
    {
        private readonly IChampionService _championService;

        public ChampionDetailViewComponent(IChampionService championService)
        {
            _championService = championService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var listChampion = await _championService.GetAllChampion();

            var userModel = Request.Cookies["User"].ToObject<User>();

            ViewData["userModel"] = userModel;

            return View(listChampion);
        }
    }
}