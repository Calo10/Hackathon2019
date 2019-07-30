using System;
namespace GFP.Models
{
    public class ElegibleModel
    {
        public string id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string program { get; set; }
        public string value { get; set; }
    }

    public class ElegibleResponseModel
    {
        public string id { get; set; }
        public bool isElegible { get; set; }
    }
}
