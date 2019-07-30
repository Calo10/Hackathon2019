using System;
using CsvHelper.Configuration.Attributes;

namespace GFP.Models
{
    public class SocialProgramModel
    {
        [Ignore]
        public string batch_id { get; set; }
        [Index(0)]
        public string id { get; set; }
        [Index(1)]
        public string first_name { get; set; }
        [Index(2)]
        public string last_name { get; set; }
        [Index(3)]
        public string email { get; set; }
        [Index(4)]
        public string program { get; set; }
        [Index(5)]
        public string date { get; set; }
        [Index(6)]
        public string value { get; set; }
        [Index(7)]
        public string IBAN { get; set; }
        [Ignore]
        public string is_Elegible { get; set; }
        [Ignore]
        public string rules_break { get; set; }

        public SocialProgramModel()
        {

        }
    }

}
