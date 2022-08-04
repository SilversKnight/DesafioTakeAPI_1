using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesafioTakeAPI_1.Model
{
    public class GitHubRepository
    {

        [JsonProperty("full_name")]
        public string full_name { get; set; }

        [JsonProperty("description")]
        public string description { get; set; }

        [JsonProperty("language")]
        public string language { get; set; }

        [JsonProperty("created_at")]
        public string created_at { get; set; }

        [JsonProperty("owner")]
        public JObject owner { get; set; }

    }
}
