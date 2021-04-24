using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Requests
{

    
    public class ParticipantRegistrationRequest
    {
        [JsonProperty("Birth_Date")]
        public DateTime Birth_Date { get; set; }

        [JsonProperty("Gender")]
        public string Gender { get; set; }

        [JsonProperty("Nationality")]
        public string Nationality { get; set; }

        [JsonProperty("Additional_Info")]
        public string AdditionalInfo { get; set; }

        public ParticipantRegistrationRequest(DateTime birth_Date, string gender, string nationality, string additionalInfo)
        {
            Birth_Date = birth_Date;
            Gender = gender;
            Nationality = nationality;
            AdditionalInfo = additionalInfo;
        }
    }
}
