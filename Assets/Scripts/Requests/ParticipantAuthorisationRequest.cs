using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Requests
{


    public class ParticipantAuthorisationRequest
    {
        [JsonProperty("Participant_Id")]
        string ID { get; set; }

        public ParticipantAuthorisationRequest(string iD)
        {
            ID = iD;
        }

    }
}
