using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [Serializable]
    public class ParticipantInExperiment
    {
        [JsonProperty("Participant_Id")]
        public int ParticipantId { get; set; }

        [JsonProperty("Intoxication")]
        public bool Intoxication { get; set; }

        [JsonProperty("Periods")]
        public bool Periods { get; set; }

        [JsonProperty("Head_Injury")]
        public bool HeadInjury { get; set; }

        [JsonProperty("Additional_Info")    ]
        public string AdditionalInfo { get; set; }

        public ParticipantInExperiment(bool intoxication, bool periods, bool headInjury, string additionalInfo)
        {
            Intoxication = intoxication;
            Periods = periods;
            HeadInjury = headInjury;
            AdditionalInfo = additionalInfo;
        }

        public ParticipantInExperiment()
        {
        }

        public override string ToString()
        {
            string res = "";
            res += "ParticipantInExperiment : {" +
                "\nParticipantId = " + ParticipantId +
                "\nIntoxication = " + Intoxication +
                "\nPeriods = " + Periods +
                "\nHeadInjury = " + HeadInjury +
                "\nAdditionalInfo = " + AdditionalInfo +
                "\n}";
            return res;
        }
    }
}