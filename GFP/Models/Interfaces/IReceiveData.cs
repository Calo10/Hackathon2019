using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GFP.Models
{
    public interface IReceiveData
    {
        Task<IEnumerable<dynamic>> GetRawSocialProgramsAsync();
        Task<IEnumerable<dynamic>> GetElegibleSocialProgramsAsync();
        Task<IEnumerable<dynamic>> GetRulesAsync();
        Task<IEnumerable<dynamic>> GetTresureForValidation();
        Task<IEnumerable<dynamic>> GetTresury();
        Task<IEnumerable<dynamic>> GetConsolidatePayments();
        Task<int> BulkSocialProgramsAsync(List<SocialProgramModel> lstSocialPrograms);
        Task<int> UpdateSocialProgramAsync(string id, SocialProgramModel socialProgram);
        Task<int> UpdateSocialProgramRulesAsync(string id, SocialProgramModel socialProgram);
        Task<int> UpdateSocialProgramTresuryValidationAsync(string id, string tresuryValidated);
    }
}
