using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [Serializable]
    public class Participant
    {

        [JsonProperty("ID")]
        string ID { get; set; }

        [JsonProperty("Gender")]
        string Gender { get; set; }

        [JsonProperty("Birth_Date")]
        DateTime BirthDate { get; set; }

        [JsonProperty("Birth_City")]
        string BirthCity { get; set; }

        [JsonProperty("Birth_Country")]
        string BirthCountry { get; set; }

        public Participant(string gender, DateTime birthDate, string birthCity, string birthCountry)
        {
            Gender = gender;
            BirthDate = birthDate;
            BirthCity = birthCity;
            BirthCountry = birthCountry;
        }



    }
}
