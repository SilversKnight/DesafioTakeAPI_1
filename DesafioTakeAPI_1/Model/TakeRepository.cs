using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioTakeAPI_1.Model
{
    public class TakeRepository
    {
        public string full_name { get; set; }
        public string description { get; set; }
        public string language { get; set; }
        public string created_at { get; set; }
        public string avatar_url { get; set; }
    }
}
