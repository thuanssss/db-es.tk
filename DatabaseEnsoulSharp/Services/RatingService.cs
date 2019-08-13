using DatabaseEnsoulSharp.Models.Database;
using DatabaseEnsoulSharp.Models.Parameter;
using DatabaseEnsoulSharp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseEnsoulSharp.Services
{
    public class RatingService : IRatingService
    {
        private readonly IFirebaseService _firebaseService;
        private readonly IResponseServerService _responseServerService;

        public RatingService(IFirebaseService firebaseService, IResponseServerService responseServerService)
        {
            _firebaseService = firebaseService;
            _responseServerService = responseServerService;
        }

        public async Task<ResponseServerService> RatingScript(ActionRatingParameter model, User user)
        {
            var users = _firebaseService.GetUsers();
            var ratings = _firebaseService.GetRatings();
            var championScripts = _firebaseService.GetChampionScript();

            await Task.WhenAll(users, ratings, championScripts);

            var listUsers = users.Result ?? new List<User>();
            var listRatings = ratings.Result ?? new List<ScriptRatingDetail>();
            var listChampionScripts = championScripts.Result ?? new List<ChampionScript>();

            var championScriptFind = listChampionScripts.FirstOrDefault(a => a.Id == model.IdScript);

            if (championScriptFind == null) return _responseServerService.ResponseError();

            var userFind = listUsers.FirstOrDefault(a => a.Email == user.Email);

            if (userFind == null) return _responseServerService.ResponseError();

            var ratingUser = listRatings.FirstOrDefault(a => a.IdUser == userFind.Id && a.IdScript == championScriptFind.Id);

            if (ratingUser == null)
            {
                ratingUser = new ScriptRatingDetail()
                {
                    CreateDate = DateTime.Now,
                    Id = listRatings.LastOrDefault()?.Id + 1 ?? 1,
                    IdScript = championScriptFind.Id,
                    IdUser = userFind.Id,
                    Point = model.Point
                };

                listRatings.Add(ratingUser);
            }
            else
            {
                ratingUser.CreateDate = DateTime.Now;
                ratingUser.IdScript = championScriptFind.Id;
                ratingUser.IdUser = userFind.Id;
                ratingUser.Point = model.Point;

                var indexof = listRatings.IndexOf(ratingUser);

                listRatings[indexof] = ratingUser;
            }

            _firebaseService.PutRatings(listRatings);

            var check = UpdateRating(listRatings, model.IdScript, listChampionScripts);

            if (check) return _responseServerService.ResponseSuccess("Rating Success !!!");

            return _responseServerService.ResponseError();
        }

        public bool UpdateRating(List<ScriptRatingDetail> ratings, int idScript, List<ChampionScript> championScripts)
        {
            ratings = ratings ?? new List<ScriptRatingDetail>();
            championScripts = championScripts ?? new List<ChampionScript>();

            var championScript = championScripts.FirstOrDefault(a => a.Id == idScript);

            if (championScript == null) return false;

            championScript.Rating = ratings.Where(a => a.IdScript == idScript).Sum(a => a.Point);
            championScript.TotalRate = ratings.Count(a => a.IdScript == idScript);

            var indexof = championScripts.IndexOf(championScript);
            championScripts[indexof] = championScript;

            _firebaseService.PutChampionScript(championScripts);

            return true;
        }
    }
}