using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GFP.Models
{
    public interface IReceiveDataProvider
    {
        Task<List<SocialProgramModel>> GetRawSocialPrograms();

        Task<bool> UploadProgramsAsync(List<SocialProgramModel> lstSocialPrograms);
    }
}
