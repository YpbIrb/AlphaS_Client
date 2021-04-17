using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Requests
{

    
    public class RegistrationRequest
    {
        [JsonProperty("Gender")]
        string Gender { get; set; }

        [JsonProperty("Birth_Date")]
        DateTime BirthDate { get; set; }

        [JsonProperty("Birth_City")]
        string BirthCity { get; set; }

        [JsonProperty("Birth_Country")]
        string BirthCountry { get; set; }

        public RegistrationRequest(string gender, DateTime birthDate, string birthCity, string birthCountry)
        {
            Gender = gender;
            BirthDate = birthDate;
            BirthCity = birthCity;
            BirthCountry = birthCountry;
        }



    }
}
