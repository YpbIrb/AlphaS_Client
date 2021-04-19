﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Requests
{


    public class AuthorisationRequest
    {
        [JsonProperty("Participant_Id")]
        string ID { get; set; }

        public AuthorisationRequest(string iD)
        {
            ID = iD;
        }

    }
}
