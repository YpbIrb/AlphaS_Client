using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleVariable
{

    [JsonProperty("Variable_Name")]
    public string VariableName { get; set; }

    [JsonProperty("Variable_DefaultValue")]
    public string VariableDefaultValue { get; set; }

    [JsonProperty("Variable_Description")]
    public string VariableDescription { get; set; }

    public ModuleVariable()
    {
    }
}
