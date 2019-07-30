using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GFP.Helper;

namespace GFP.Models
{
    public class ReceiveData : IReceiveData
    {
        #region SQL's
        private const string SP_GetRawData = "SP_ReturnRaw";
        private const string SP_BulkSocialPrograms = "SP_BulkSocialPrograms";
        private const string SP_UpdateSocialPrograms = "SP_UpdateSocialPrograms";
        #endregion

        private readonly string _conString;
        private readonly DbConnectionHelper.ConnType _conType = DbConnectionHelper.ConnType.MySql;

        public ReceiveData(string conString)
        {
            _conString = conString;
        }

        public async Task<int> BulkSocialProgramsAsync(List<SocialProgramModel> lstSocialPrograms)
        {
            var list = lstSocialPrograms.ConvertAll(x => (object)x);

            return await DbConnectionHelper.BulkTransaction(_conString, _conType, SP_BulkSocialPrograms, list, true);
        }

        public async Task<IEnumerable<dynamic>> GetRawSocialProgramsAsync()
        {
            return await DbConnectionHelper.QueryAsync(_conString, _conType, SP_GetRawData, null, true);
        }

        public async Task<int> UpdateSocialProgramAsync(string id, SocialProgramModel socialProgram)
        {
            return await DbConnectionHelper.ExecuteAsync(_conString, _conType, SP_UpdateSocialPrograms, new
            {
                id,
                socialProgram.Is_Elegible,
                socialProgram.batch_id
            }, true);
        }
    }
}
