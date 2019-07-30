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
        Task<int> BulkSocialProgramsAsync(List<SocialProgramModel> lstSocialPrograms);
        Task<int> UpdateSocialProgramAsync(string id, SocialProgramModel socialProgram);
        Task<int> UpdateSocialProgramRulesAsync(string id, SocialProgramModel socialProgram);
    }
}
