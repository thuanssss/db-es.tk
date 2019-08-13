using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseEnsoulSharp.Models.Database;
using DatabaseEnsoulSharp.Models.Parameter;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DatabaseEnsoulSharp.Services.Interface
{
    public interface IScriptInfoService
    {
        Task<bool> CreateScript(ActionCreateScriptInfoParameter model);
        Task<List<ScriptInfo>> GetAllScript();
        Task<List<SelectListItem>> GetAllScriptForDropdown();
    }
}
