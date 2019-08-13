using DatabaseEnsoulSharp.Models.Database;
using DatabaseEnsoulSharp.Models.Parameter;
using DatabaseEnsoulSharp.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseEnsoulSharp.Services
{
    public class LoginService : ILoginService
    {
        private readonly IFirebaseService _firebaseService;

        public LoginService(IFirebaseService firebaseService)
        {
            _firebaseService = firebaseService;
        }

        public async Task<bool> Login(LoginParameter parameterLogin)
        {
            var users = await _firebaseService.GetUsers() ?? new List<User>();
            var user = users.FirstOrDefault(a => a.Email == parameterLogin.Email);

            if (user != null) return true;

            user = new User
            {
                CreateDate = DateTime.Now,
                Id = users.LastOrDefault()?.Id + 1 ?? 1,
                FirstName = parameterLogin.FirstName,
                LastName = parameterLogin.LastName,
                Email = parameterLogin.Email,
                Status = "Actived",
            };

            users.Add(user);

            _firebaseService.PutUsers(users);

            return true;
        }

        public bool ClearCache(string username, string password)
        {
            if (username == "ss" && password == "ss")
            {
                return true;
            }

            return false;
        }
    }
}