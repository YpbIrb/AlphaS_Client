﻿using Newtonsoft.Json;
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

        [JsonProperty("Participant_Id")]
        public string ParticipantId { get; set; }

        [JsonProperty("Gender")]
        public string Gender { get; set; }

        [JsonProperty("Birth_Date")]
        public DateTime BirthDate { get; set; }

        [JsonProperty("Nationality")]
        public string Nationality { get; set; }

        [JsonProperty("Additional_Info")]
        public string AdditionalInfo { get; set; }

        public Participant(string gender, DateTime birthDate, string nationality, string additionalInfo)
        {
            Gender = gender;
            BirthDate = birthDate;
            Nationality = nationality;
            AdditionalInfo = additionalInfo;
        }



    }
}
