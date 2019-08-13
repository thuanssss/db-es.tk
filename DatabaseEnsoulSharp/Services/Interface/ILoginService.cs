using DatabaseEnsoulSharp.Models.Parameter;
using System.Threading.Tasks;

namespace DatabaseEnsoulSharp.Services.Interface
{
    public interface ILoginService
    {
        Task<bool> Login(LoginParameter parameterLogin);

        bool ClearCache(string username, string password);
    }
}