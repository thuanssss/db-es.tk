using DatabaseEnsoulSharp.Models.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatabaseEnsoulSharp.Models.Parameter;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DatabaseEnsoulSharp.Services.Interface
{
    public interface IChampionService
    {
        Task<bool> CreateChampion(ActionCreateChampionParameter model);
        Task<List<Champion>> GetAllChampion();
        Task<List<SelectListItem>> GetAllChampionForDropdown();
    }
}
