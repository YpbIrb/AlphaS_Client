using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [Serializable]
    public class Experiment
    {
        [JsonProperty("Experiment_Id")]
        public int ExperimentId { get; set; }

        [JsonProperty("Operator_Id")]
        public int OperatorId { get; set; }

        [JsonProperty("Preset_Name")]
        public string PresetName { get; set; }

        [JsonProperty("First_Participant")]
        public ParticipantInExperiment FirstParticipant { get; set; }

        [JsonProperty("Second_Participant")]
        public ParticipantInExperiment SecondParticipant { get; set; }

        [JsonProperty("Modules")]
        public List<ModuleInExperiment> Modules { get; set; }

        [JsonProperty("Start_Time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("Finish_Time")]
        public DateTime FinishTime { get; set; }

        
        public override string ToString()
        {
            string res = base.ToString();
            res += "\nExperiment : {" +
                "\nExperimentId = " + ExperimentId +
                "\nOperatorId = " + OperatorId +
                "\nFirstParticipant = " + FirstParticipant +
                "\nSecondParticipant = " + SecondParticipant +
                "\nModules = {";
            if (Modules != null)
            {
                foreach (ModuleInExperiment module in Modules)
                {
                    res += module.ToString();
                }
            }

            res += "\n}" + 
                "\nStartTime = " + StartTime +
                "\nFinishTime = " + FinishTime +
                "\n}";
            return res;
        }

    }
}

