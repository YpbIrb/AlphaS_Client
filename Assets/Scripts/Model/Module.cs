using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.Model
{
    [Serializable]
    public class Module
    {

        [JsonProperty("Module_Id")]
        public int ModuleId { get; set; }

        [JsonProperty("Module_Name")]
        public string ModuleName { get; set; }

        [JsonProperty("Module_Type_Name")]
        public string ModuleTypeName { get; set; }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Input_Variables")]
        public List<ModuleVariable> InputVariables { get; set; }

        [JsonProperty("Output_Variables")]
        public List<ModuleVariable> OutputVariables { get; set; }

        [JsonProperty("Path_To_Exe")]
        public string PathToExe { get; set; }

    }

}