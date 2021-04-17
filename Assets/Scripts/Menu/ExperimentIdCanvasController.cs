using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class ExperimentIdCanvasController : CanvasController
    {
        protected void Awake()
        {
            Debug.Log("In Awake in ExperimentIdCanvasController");
            this.menuCanvasType = MenuCanvasType.ExperimentIdEnteringMenu;
        }

        public string GetExperimentId()
        {
            return "ExperimentId";
        }
    }
}