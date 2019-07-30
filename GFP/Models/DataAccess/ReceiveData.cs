using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GFP.Helper;

namespace GFP.Models
{
    public class ReceiveData : IReceiveData
    {
        #region SQL's
        private const string SP_GetRawData = "SP_GetRawData";
        private const string SP_BulkSocialPrograms = "SP_BulkSocialPrograms";
        private const string SP_UpdateSocialPrograms = "SP_UpdateSocialPrograms";
        private const string SP_GetElegibleData = "SP_GetElegibleData";
        private const string SP_GetRules = "SP_GetRules";
        private const string SP_GetTresury = "SP_GetRules";
        private const string SP_UpdateSocialProgramsRules = "SP_UpdateSocialProgramsRules";
        private const string SP_GetTresuryForValidation = "SP_GetTresuryForValidation";
        private const string SP_UpdateSocialProgramsTresuryValidation = "SP_UpdateSocialProgramsTresuryValidation";
        private const string SP_GetTresuryStatus = "SP_GetTresuryStatus";
        private const string SP_GetPaymentConsolidate = "SP_GetPaymentConsolidate";
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

        public async Task<IEnumerable<dynamic>> GetConsolidatePayments()
        {
            return await DbConnectionHelper.QueryAsync(_conString, _conType, SP_GetPaymentConsolidate, null, true);
        }

        public async Task<IEnumerable<dynamic>> GetElegibleSocialProgramsAsync()
        {
            return await DbConnectionHelper.QueryAsync(_conString, _conType, SP_GetElegibleData, null, true);
        }

        public async Task<IEnumerable<dynamic>> GetRawSocialProgramsAsync()
        {
            return await DbConnectionHelper.QueryAsync(_conString, _conType, SP_GetRawData, null, true);
        }

        public async Task<IEnumerable<dynamic>> GetRulesAsync()
        {
            return await DbConnectionHelper.QueryAsync(_conString, _conType, SP_GetRules, null, true);
        }

        public async Task<IEnumerable<dynamic>> GetTresureForValidation()
        {
            return await DbConnectionHelper.QueryAsync(_conString, _conType, SP_GetTresuryForValidation, null, true);
        }

        public async Task<IEnumerable<dynamic>> GetTresury()
        {
            return await DbConnectionHelper.QueryAsync(_conString, _conType, SP_GetTresuryStatus, null, true);
        }

        public async Task<int> UpdateSocialProgramAsync(string id, SocialProgramModel socialProgram)
        {
            return await DbConnectionHelper.ExecuteAsync(_conString, _conType, SP_UpdateSocialPrograms, new
            {
                id,
                socialProgram.is_Elegible,
                socialProgram.batch_id
            }, true);
        }

        public async Task<int> UpdateSocialProgramRulesAsync(string id, SocialProgramModel socialProgram)
        {
            return await DbConnectionHelper.ExecuteAsync(_conString, _conType, SP_UpdateSocialProgramsRules, new
            {
                id,
                socialProgram.rules_break,
                socialProgram.batch_id
            }, true);
        }

        public async Task<int> UpdateSocialProgramTresuryValidationAsync(string id, string tresuryValidated)
        {
            return await DbConnectionHelper.ExecuteAsync(_conString, _conType, SP_UpdateSocialProgramsTresuryValidation, new
            {
                batch_id = id,
                tresury_validated = tresuryValidated
            }, true);
        }
    }
}
