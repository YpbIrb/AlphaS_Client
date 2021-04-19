using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class ExperimentIdCanvasController : CanvasController
    {

        [SerializeField]
        GameObject IDInputField;

        protected void Awake()
        {
            Debug.Log("In Awake in ExperimentIdCanvasController");
            this.menuCanvasType = MenuCanvasType.ExperimentIdEnteringMenu;
        }

        public string GetExperimentId()
        {
            return IDInputField.GetComponent<TextMeshProUGUI>().text.Trim();
        }
    }
}