using DatabaseEnsoulSharp.Models.Database;
using DatabaseEnsoulSharp.Models.Parameter;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseEnsoulSharp.Services.Interface
{
    public interface IRatingService
    {
        Task<ResponseServerService> RatingScript(ActionRatingParameter model, User user);

        bool UpdateRating(List<ScriptRatingDetail> ratings, int idScript, List<ChampionScript> championScripts);
    }
}