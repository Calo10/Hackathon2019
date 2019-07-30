using System;
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

        public async Task<bool> UploadProgramsAsync(List<SocialProgramModel> lstSocialPrograms)
        {
            //Set Id for each batch
            Guid guid = Guid.NewGuid();

            foreach (var item in lstSocialPrograms)
            {
                item.batch_id = guid.ToString();
            }

            var result = await _receiveData.BulkSocialProgramsAsync(lstSocialPrograms);

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
                        value = item.value == null ? 0 : (int)item.value
                    });

                    item.Is_Elegible = response.isElegible == false ? "N" : "S";

                    await _receiveData.UpdateSocialProgramAsync(item.id, item);
                }
            }

            return result == 0 ? false : true;
        }


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

        public async Task<bool> UpdateSocialProgram(string Id, SocialProgramModel socialProgram)
        {
            var result = await _receiveData.UpdateSocialProgramAsync(Id, socialProgram);

            return result == 0 ? false : true;
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
                program = data.program,
                date = data.date,
                email = data.email,
                IBAN = data.IBAN,
                value = data.value
            };
        }


    }
}
