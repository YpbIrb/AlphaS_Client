using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Model
{
    [Serializable]
    public class ModuleInExperiment
    {
        [JsonProperty("Module_Name")]
        public string ModuleName { get; set; }

        [JsonProperty("Module_Order")]
        public int ModuleOrder { get; set; }

        [JsonProperty("Input_Values")]
        public Dictionary<string, string> InputValues { get; set; }

        [JsonProperty("Output_Values")]
        public Dictionary<string, string> OutputValues { get; set; }

        [JsonProperty("Start_Time")]
        public DateTime StartTime { get; set; }

        [JsonProperty("Finish_Time")]
        public DateTime FinishTime { get; set; }

        public override string ToString()
        {
            string res = "";
            res += "ModuleInExperiment : {" +
                "\nModuleName = " + ModuleName +
                "\nModuleOrder = " + ModuleOrder +
                "\nStartTime = " + StartTime +
                "\nFinishTime = " + FinishTime +
                "\nInputValues = {";
            if (InputValues != null)
            {
                foreach (KeyValuePair<string, string> pair in InputValues)
                {
                    res += "\n " + pair.Key + " : " + pair.Value;
                }
            }
            
            res += "}" +
            "\nOutputValues = {";
            if (InputValues != null)
            {
                foreach (KeyValuePair<string, string> pair in OutputValues)
                {
                    res += "\n " + pair.Key + " : " + pair.Value;
                }
            }
            res += "}" +
                "\n}";

            return res;
        }
    }
}
