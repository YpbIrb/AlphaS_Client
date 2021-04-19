using System.Collections;
using System.Collections.Generic;
using UnityEngine;




namespace Assets.Scripts.Menu
{
    public class ParticipantIdentificationTypeCanvasController : CanvasController
    {

        protected void Awake()
        {
            Debug.Log("In Awake in IdentificationTypeCanvasController");
            this.menuCanvasType = MenuCanvasType.ParticipantIdentificationTypeChoiceMenu;
        }

     
    }
}