using DatabaseEnsoulSharp.Models.Database;
using DatabaseEnsoulSharp.Services.Interface;
using FireSharp;
using FireSharp.Config;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseEnsoulSharp.Services
{
    public class FirebaseService : IFirebaseService
    {
        private readonly FirebaseConfig _config;
        private readonly FirebaseClient _firebaseClient;

        public FirebaseService()
        {
            _config = new FirebaseConfig { AuthSecret = "#####", BasePath = "###" };
            _firebaseClient = new FirebaseClient(_config);
        }

        public async Task<List<Champion>> GetChampions()
        {
            var response = await _firebaseClient.GetAsync("Champion");
            return response.ResultAs<List<Champion>>();
        }

        public async Task<List<ScriptInfo>> GetScripts()
        {
            var response = await _firebaseClient.GetAsync("ScriptInfo");
            return response.ResultAs<List<ScriptInfo>>();
        }

        public async Task<List<ChampionScript>> GetChampionScript()
        {
            var response = await _firebaseClient.GetAsync("ChampionScript");
            return response.ResultAs<List<ChampionScript>>();
        }

        public async Task<List<User>> GetUsers()
        {
            var response = await _firebaseClient.GetAsync("User");
            return response.ResultAs<List<User>>();
        }

        public bool PutUsers(List<User> users)
        {
            var response = _firebaseClient.Set("User", users);
            return true;
        }

        public async Task<List<ScriptRatingDetail>> GetRatings()
        {
            var response = await _firebaseClient.GetAsync("ScriptRatingDetail");
            return response.ResultAs<List<ScriptRatingDetail>>();
        }

        public bool PutRatings(List<ScriptRatingDetail> ratings)
        {
            var response = _firebaseClient.Set("ScriptRatingDetail", ratings);
            return true;
        }

        public bool PutChampionScript(List<ChampionScript> championScripts)
        {
            var response = _firebaseClient.Set("ChampionScript", championScripts);
            return true;
        }

        public bool PutChampions(List<Champion> champion)
        {
            var response = _firebaseClient.Set("Champion", champion);
            return true;
        }

        public bool PutScriptInfos(List<ScriptInfo> scriptInfos)
        {
            var response = _firebaseClient.Set("ScriptInfo", scriptInfos);
            return true;
        }
    }
}