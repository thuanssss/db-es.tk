using DatabaseEnsoulSharp.Models.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseEnsoulSharp.Services.Interface
{
    public interface IFirebaseService
    {
        Task<List<Champion>> GetChampions();

        Task<List<ScriptInfo>> GetScripts();

        Task<List<ChampionScript>> GetChampionScript();

        Task<List<User>> GetUsers();

        bool PutUsers(List<User> users);

        Task<List<ScriptRatingDetail>> GetRatings();

        bool PutRatings(List<ScriptRatingDetail> ratings);

        bool PutChampionScript(List<ChampionScript> championScripts);

        bool PutChampions(List<Champion> champion);
        bool PutScriptInfos(List<ScriptInfo> scriptInfos);
    }
}