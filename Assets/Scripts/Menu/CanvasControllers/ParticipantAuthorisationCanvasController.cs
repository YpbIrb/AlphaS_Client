using Assets.Scripts.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



namespace Assets.Scripts.Menu
{

    public class ParticipantAuthorisationCanvasController : CanvasController
    {
        [SerializeField]
        GameObject IDInputField;

        protected void Awake()
        {
            Debug.Log("In Awake in AuthorisationCanvasController");
            this.menuCanvasType = MenuCanvasType.ParticipantAuthorisationMenu;
        }

        public AuthorisationRequest GetAuthorisationInfo()
        {
            return new AuthorisationRequest(IDInputField.GetComponent<TextMeshProUGUI>().text);
        }


    }
}