using System;
using DatabaseEnsoulSharp.Models.Database;
using DatabaseEnsoulSharp.Models.Parameter;
using DatabaseEnsoulSharp.Services.Interface;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseEnsoulSharp.Models.Extensions;

namespace DatabaseEnsoulSharp.Services
{
    public class ChampionScriptService : IChampionScriptService
    {
        private readonly IFirebaseService _firebaseService;
        private readonly ICacheService _cacheService;
        private readonly IChampionService _championService;
        private readonly IScriptInfoService _scriptInfoService;

        private const string KeyChampion = "Champion-";
        private const string KeyAllChampion = "AllChampion";

        public ChampionScriptService(IFirebaseService firebaseService, ICacheService cacheService, IChampionService championService, IScriptInfoService scriptInfoService)
        {
            _firebaseService = firebaseService;
            _cacheService = cacheService;
            _championService = championService;
            _scriptInfoService = scriptInfoService;
        }

        public async Task<bool> ChangeStatusScript(ActionStatusParameter model)
        {
            var championScripts = await _firebaseService.GetChampionScript() ?? new List<ChampionScript>();

            var champion = championScripts.FirstOrDefault(a => a.Id == model.IdScript);

            if (champion == null) return false;

            champion.Status = champion.Status == "Outdated" ? "Updated" : "Outdated";

            var index = championScripts.IndexOf(champion);

            championScripts[index] = champion;

            _firebaseService.PutChampionScript(championScripts);

            return true;
        }

        public async Task<List<ChampionScript>> GetListChampionScript(int idChampion)
        {
            var arrayScriptInfo = _scriptInfoService.GetAllScript();
            var arrayChampion = _championService.GetAllChampion();

            await Task.WhenAll(arrayScriptInfo, arrayChampion);

            var arrayChampionScript = _cacheService.GetCache<List<ChampionScript>>($"{KeyChampion}{idChampion}");

            if (arrayChampionScript == null)
            {
                var data = await _firebaseService.GetChampionScript();
                arrayChampionScript = new List<ChampionScript>();

                foreach (var item in data.Where(a => a.IdChampion == idChampion))
                {
                    item.Champion = item.ChampionExtension(arrayChampion.Result);
                    item.ScriptInfo = item.ScriptInfoExtension(arrayScriptInfo.Result);

                    arrayChampionScript.Add(item);
                }

                arrayChampionScript = arrayChampionScript.OrderByDescending(a => a.Status)
                    .ThenByDescending(a => (Convert.ToDouble(a.Rating) / Convert.ToDouble(a.TotalRate))).ToList();

                _cacheService.SetCache($"{KeyChampion}{idChampion}", arrayChampionScript);
            }

            return arrayChampionScript;
        }


        public async Task<List<ChampionScript>> GetAllChampionScript()
        {
            var arrayScriptInfo = _scriptInfoService.GetAllScript();
            var arrayChampion = _championService.GetAllChampion();

            await Task.WhenAll(arrayScriptInfo, arrayChampion);

            var arrayChampionScript = _cacheService.GetCache<List<ChampionScript>>(KeyAllChampion);

            if (arrayChampionScript == null)
            {
                var data = await _firebaseService.GetChampionScript();
                arrayChampionScript = new List<ChampionScript>();

                foreach (var item in data)
                {
                    item.Champion = item.ChampionExtension(arrayChampion.Result);
                    item.ScriptInfo = item.ScriptInfoExtension(arrayScriptInfo.Result);

                    arrayChampionScript.Add(item);
                }

                _cacheService.SetCache(KeyAllChampion, arrayChampionScript);
            }

            return arrayChampionScript;
        }

        public async Task<bool> CreateChampionScript(ActionCreateChampionScriptParameter model)
        {
            var championScripts = await GetAllChampionScript() ?? new List<ChampionScript>();
            championScripts = championScripts.OrderBy(a => a.Id).ToList();

            var script = championScripts.FirstOrDefault(a => a.IdChampion == model.IdChampion && a.IdScriptInfo == model.IdScriptInfo);

            if (script != null) return true;

            script = new ChampionScript()
            {
                Id = championScripts.LastOrDefault()?.Id + 1 ?? 1,
                Name =  model.IdChampion.ToString(),
                IdChampion =  model.IdChampion,
                IdScriptInfo =  model.IdScriptInfo,
                Rating = 0,
                Status = "Updated",
                TotalRate = 0,
                Type = "FREE"
            };

            championScripts.Add(script);

            _firebaseService.PutChampionScript(championScripts);

            _cacheService.ClearCacheByKey(KeyAllChampion);
            _cacheService.ClearCacheByKey($"{KeyChampion}{model.IdChampion}");

            return true;
        }
    }
}