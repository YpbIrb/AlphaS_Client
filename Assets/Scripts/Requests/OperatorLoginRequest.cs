using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Requests
{
    [Serializable]
    public class OperatorLoginRequest
    {
        [JsonProperty("User_Name")]
        public string UserName { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
    }
}
