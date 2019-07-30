using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GFP.Models
{
    public interface IReceiveData
    {
        Task<IEnumerable<dynamic>> GetRawSocialProgramsAsync();
        Task<int> BulkSocialProgramsAsync(List<SocialProgramModel> lstSocialPrograms);
        Task<int> UpdateSocialProgramAsync(string id, SocialProgramModel socialProgram);
    }
}
