using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseEnsoulSharp.Models.Database;
using DatabaseEnsoulSharp.Models.Parameter;
using DatabaseEnsoulSharp.Services.Interface;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DatabaseEnsoulSharp.Services
{
    public class ScriptInfoService : IScriptInfoService
    {
        private readonly IFirebaseService _firebaseService;
        private readonly ICacheService _cacheService;
        private const string KeyScriptInfo = "scriptInfo";

        public ScriptInfoService(IFirebaseService firebaseService, ICacheService cacheService)
        {
            _firebaseService = firebaseService;
            _cacheService = cacheService;
        }

        public async Task<bool> CreateScript(ActionCreateScriptInfoParameter model)
        {
            var scripts = await GetAllScript() ?? new List<ScriptInfo>();
            scripts = scripts.OrderBy(a => a.Id).ToList();

            var script = scripts.FirstOrDefault(a => a.Author == model.Author);

            if (script != null) return true;

            script = new ScriptInfo()
            {
                Name = model.Name,
                Id = scripts.LastOrDefault()?.Id + 1 ?? 1,
                Author = model.Author,
                Link = model.Link
            };

            scripts.Add(script);

            _firebaseService.PutScriptInfos(scripts);

            _cacheService.ClearCacheByKey(KeyScriptInfo);

            return true;
        }

        public async Task<List<ScriptInfo>> GetAllScript()
        {
            var arrayScriptInfo = _cacheService.GetCache<List<ScriptInfo>>(KeyScriptInfo);

            if (arrayScriptInfo == null)
            {
                arrayScriptInfo = await _firebaseService.GetScripts();
                _cacheService.SetCache(KeyScriptInfo, arrayScriptInfo);
            }

            return arrayScriptInfo;
        }

        public async Task<List<SelectListItem>> GetAllScriptForDropdown()
        {
            var data = await GetAllScript() ?? new List<ScriptInfo>();

            return data.Select(a => new SelectListItem { Value = a.Id.ToString(), Text = a.Author }).ToList();
        }
    }
}
