using DatabaseEnsoulSharp.Models.Database;
using DatabaseEnsoulSharp.Models.Parameter;
using DatabaseEnsoulSharp.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DatabaseEnsoulSharp.Services
{
    public class ChampionService : IChampionService
    {
        private readonly IFirebaseService _firebaseService;
        private readonly ICacheService _cacheService;
        private const string KeyListChampion = "ListChampion";

        public ChampionService(IFirebaseService firebaseService, ICacheService cacheService)
        {
            _firebaseService = firebaseService;
            _cacheService = cacheService;
        }

        public async Task<bool> CreateChampion(ActionCreateChampionParameter model)
        {
            var champions = await GetAllChampion() ?? new List<Champion>();
            champions = champions.OrderBy(a => a.Id).ToList();

            var championFind = champions.FirstOrDefault(a => a.Name == model.Name);

            if (championFind != null) return true;

            championFind = new Champion
            {
                Name = model.Name,
                Id = champions?.LastOrDefault()?.Id + 1 ?? 1
            };

            champions.Add(championFind);

            _firebaseService.PutChampions(champions);

            _cacheService.ClearCacheByKey(KeyListChampion);

            return true;
        }

        public async Task<List<Champion>> GetAllChampion()
        {
            var listChampion = _cacheService.GetCache<List<Champion>>(KeyListChampion);

            if (listChampion == null)
            {
                listChampion = await _firebaseService.GetChampions();
                listChampion = listChampion.OrderBy(a => a.Name).ToList();

                _cacheService.SetCache(KeyListChampion, listChampion);
            }

            return listChampion;
        }

        public async Task<List<SelectListItem>> GetAllChampionForDropdown()
        {
            var champions = await GetAllChampion() ?? new List<Champion>();
            return champions.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Name }).ToList();
        }
    }
}
