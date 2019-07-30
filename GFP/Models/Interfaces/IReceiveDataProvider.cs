using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GFP.Models
{
    public interface IReceiveDataProvider
    {
        Task<List<SocialProgramModel>> GetRawSocialPrograms();

        Task<List<SocialProgramModel>> GetElegibleSocialPrograms();

        Task<List<SocialProgramModel>> GetRuleSocialPrograms();

        Task<bool> UploadProgramsAsync(List<SocialProgramModel> lstSocialPrograms);

        Task<List<RulesModel>> GetRules();
    }
}
