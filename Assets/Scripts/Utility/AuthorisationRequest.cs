using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Utility
{


    public class AuthorisationRequest
    {
        [JsonProperty("ID")]
        string ID { get; set; }

        public AuthorisationRequest(string iD)
        {
            ID = iD;
        }

    }
}
