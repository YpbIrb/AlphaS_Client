using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    public class ParticipantInExperiment
    {
        [JsonProperty("Intoxication")]
        public bool intoxication { get; set; }
    }
}