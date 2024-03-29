﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using GFP.Models;
using Newtonsoft.Json;

namespace GFP.Services
{
    public class ReceiveDataProvider : IReceiveDataProvider
    {

        private readonly IReceiveData _receiveData;

        public ReceiveDataProvider(IReceiveData receiveData)
        {
            _receiveData = receiveData;
        }

        public async Task<List<SocialProgramModel>> GetRawSocialPrograms()
        {
            var data = await _receiveData.GetRawSocialProgramsAsync();

            if (data == null)
                throw new ArgumentException(string.Format("Format Error"));

            return data.Select(ConvertFromData).ToList(); ;
        }

        public async Task<List<SocialProgramModel>> GetElegibleSocialPrograms()
        {
            var data = await _receiveData.GetElegibleSocialProgramsAsync();

            if (data == null)
                throw new ArgumentException(string.Format("Format Error"));

            return data.Select(ConvertFromData).ToList(); ;
        }

        public async Task<List<SocialProgramModel>> GetRuleSocialPrograms()
        {
            var data = await _receiveData.GetRawSocialProgramsAsync();

            if (data == null)
                throw new ArgumentException(string.Format("Format Error"));

            return data.Select(ConvertFromData).ToList();
        }

        public async Task<List<TresuryBatchValidationModel>> GetTresury()
        {
            var data = await _receiveData.GetTresury();

            if (data == null)
                throw new ArgumentException(string.Format("Format Error"));

            return data.Select(ConvertFromDataTresuryValidation).ToList();
        }

        public async Task<List<ConsolidatePaymentModel>> GetConsolidatePayments()
        {
            var data = await _receiveData.GetConsolidatePayments();

            if (data == null)
                throw new ArgumentException(string.Format("Format Error"));

            return data.Select(ConvertFromDataConsolidatePayments).ToList();
        }

        public async Task<bool> UploadProgramsAsync(List<SocialProgramModel> lstSocialPrograms)
        {
            //Set Id for each batch
            Guid guid = Guid.NewGuid();

            foreach (var item in lstSocialPrograms)
            {
                item.batch_id = guid.ToString();
            }

            //STEP 1 Insert file
            var result = await _receiveData.BulkSocialProgramsAsync(lstSocialPrograms);


            //STEP 2 Consult Elegibles
            if (result != 0)
            {
                foreach (var item in lstSocialPrograms)
                {
                    var response = ProcessEligibles(new ElegibleModel
                    {
                        id = item.id,
                        first_name = item.first_name,
                        last_name = item.last_name,
                        program = item.program,
                        value = item.value
                    });

                    item.is_Elegible = response.isElegible == false ? "N" : "S";

                    await _receiveData.UpdateSocialProgramAsync(item.id, item);
                }
            }

            //STEP 3 Run Rules
            foreach (var item in lstSocialPrograms)
            {
                var validation = RunRules(item, lstSocialPrograms);

                if (validation != "")
                {
                    item.rules_break = validation;

                    await _receiveData.UpdateSocialProgramRulesAsync(item.id, item);
                }
            }

            //STEP 4 Verify against Tresury
            var respose = await TresuryValidation();

            //STEP 5 Consolidate Payments
            if (respose)
            {

            }

            return result == 0 ? false : true;
        }

        public async Task<List<RulesModel>> GetRules()
        {
            var data = await _receiveData.GetRulesAsync();

            if (data == null)
                throw new ArgumentException(string.Format("Format Error"));

            return data.Select(ConvertFromDataRules).ToList(); ;
        }

        public async Task<bool> UpdateSocialProgram(string Id, SocialProgramModel socialProgram)
        {
            var result = await _receiveData.UpdateSocialProgramAsync(Id, socialProgram);

            return result == 0 ? false : true;
        }

        public async Task<bool> TresuryValidation()
        {
            var data = await _receiveData.GetTresureForValidation();
            var response = 0;

            if (data == null)
                throw new ArgumentException(string.Format("Format Error"));

            var lstTresuryValidationBatch = data.Select(ConvertFromDataTresuryValidation).ToList();

            foreach (var item in lstTresuryValidationBatch)
            {
                var ans = TresuryConsult(item);

                if (ans)
                {
                    response = await _receiveData.UpdateSocialProgramTresuryValidationAsync(item.BatchId, "S");
                }
                else
                {
                    response = await _receiveData.UpdateSocialProgramTresuryValidationAsync(item.BatchId, "N");
                }

            }

            return response == 0 ? false : true;
        }

        private bool TresuryConsult(TresuryBatchValidationModel tresuryBatchValidationModel)
        {
            //using (HttpClient client = new HttpClient())
            //{
            //
            //    var uri = new Uri("******TRESURY API******");
            //
            //    var json = JsonConvert.SerializeObject(
            //        new 
            //        {
            //            EntityId = tresuryBatchValidationModel.EntityID,
            //            Amount = tresuryBatchValidationModel.TotalAmount
            //        }
            //    );
            //
            //    var content = new StringContent(json, Encoding.UTF8, "application/json");
            //    HttpResponseMessage response = client.PostAsync(uri, content).Result;
            //    string ans = response.Content.ReadAsStringAsync().Result;
            //
            //    return JsonConvert.DeserializeObject<ElegibleResponseModel>(ans);
            //}

            return true;
        }

        #region Private Methods
        private ElegibleResponseModel ProcessEligibles(ElegibleModel elegible)
        {
            using (HttpClient client = new HttpClient())
            {

                var uri = new Uri("https://89p2wz20d0.execute-api.us-east-1.amazonaws.com/prod/programeligible");

                var json = JsonConvert.SerializeObject(elegible);

                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = client.PostAsync(uri, content).Result;
                string ans = response.Content.ReadAsStringAsync().Result;

                return JsonConvert.DeserializeObject<ElegibleResponseModel>(ans);
            }
        }

        private string RunRules(SocialProgramModel socialProgram, List<SocialProgramModel> lstSocialProgram)
        {
            string validation = string.Empty;
            List<RulesModel> lstRules = GetRules().Result;

            //Max Amount Rule
            if (lstRules.FirstOrDefault(x => x.IdRules == 1).Active == "S")
            {
                if (int.Parse(socialProgram.value) >= int.Parse(lstRules.FirstOrDefault(x => x.IdRules == 1).Parameter))
                    validation = "1,";
            }

            //Repeat Rule
            if (lstRules.FirstOrDefault(x => x.IdRules == 2).Active == "S")
            {
                var query = lstSocialProgram.GroupBy(x => x)
                                  .Where(g => g.Count() > 1)
                                  .Select(y => y.Key)
                                  .ToList().Count();

                if (query > 0)
                    validation += "2,";
            }

            //CUIDO Rule
            if (lstRules.FirstOrDefault(x => x.IdRules == 3).Active == "S")
            {

            }

            //Min Date
            if (lstRules.FirstOrDefault(x => x.IdRules == 4).Active == "S")
            {
                if (DateTime.Parse(socialProgram.date) < DateTime.Parse(lstRules.FirstOrDefault(x => x.IdRules == 4).Parameter))
                {
                    validation += "4,";
                }

            }

            //Max Date
            if (lstRules.FirstOrDefault(x => x.IdRules == 4).Active == "S")
            {
                if (DateTime.Parse(socialProgram.date) > DateTime.Parse(lstRules.FirstOrDefault(x => x.IdRules == 5).Parameter))
                {
                    validation += "5,";
                }

            }

            return validation;
        }

        private static TresuryBatchValidationModel ConvertFromDataTresuryValidation(dynamic data)
        {
            if (data == null)
                return null;

            return new TresuryBatchValidationModel
            {
                BatchId = data.batch_id,
                TotalAmount = data.total_amount.ToString(),
                TresuryValidated = data.tresury_validated
            };
        }


        private static RulesModel ConvertFromDataRules(dynamic data)
        {
            if (data == null)
                return null;

            return new RulesModel
            {
                IdRules = data.idrules,
                Name = data.Name,
                Parameter = data.Parameter,
                Description = data.Description,
                Active = data.Active
            };
        }

        private static SocialProgramModel ConvertFromData(dynamic data)
        {
            if (data == null)
                return null;

            return new SocialProgramModel
            {
                id = data.id,
                first_name = data.first_name,
                last_name = data.last_name,
                program = data.Program,
                date = data.Date,
                email = data.email,
                IBAN = data.IBAN,
                value = data.Value,
                is_Elegible = data.is_Elegible,
                rules_break = data.rules_break
            };
        }

        private static ConsolidatePaymentModel ConvertFromDataConsolidatePayments(dynamic data)
        {
            if (data == null)
                return null;

            return new ConsolidatePaymentModel
            {
                Id = data.id,
                FirstName = data.first_name,
                total_amount = data.total_amount.ToString(),
                IBAN = data.IBAN
            };
        }

        #endregion
    }
}
