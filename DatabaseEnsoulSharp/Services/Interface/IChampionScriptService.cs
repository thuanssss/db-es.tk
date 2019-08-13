using System.Collections.Generic;
using DatabaseEnsoulSharp.Models.Parameter;
using System.Threading.Tasks;
using DatabaseEnsoulSharp.Models.Database;

namespace DatabaseEnsoulSharp.Services.Interface
{
    public interface IChampionScriptService
    {
        Task<bool> ChangeStatusScript(ActionStatusParameter model);

        Task<List<ChampionScript>> GetListChampionScript(int idChampion);
        Task<List<ChampionScript>> GetAllChampionScript();
        Task<bool> CreateChampionScript(ActionCreateChampionScriptParameter model);

    }
}