using Assets.Scripts.Menu;
using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Assets.Scripts.Menu
{

    public class ParticipantInExperimentCanvasController : CanvasController
    {

        [SerializeField]
        GameObject IntoxicationInpitField;

        protected void Awake()
        {
            Debug.Log("In Awake in IdentificationTypeCanvasController");
            this.menuCanvasType = MenuCanvasType.ParticipantInExperimentMenu;
        }

        public ParticipantInExperiment GetParticipantInExperiment()
        {
            ParticipantInExperiment res = new ParticipantInExperiment();
            //todo
            return res;
        }

    }
}