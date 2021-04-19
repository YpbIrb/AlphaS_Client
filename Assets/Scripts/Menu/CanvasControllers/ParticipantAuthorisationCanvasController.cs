using Assets.Scripts.Requests;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public int GetAuthorisationId()
        {

            int part_id;
            string id_string = new string(IDInputField.GetComponent<TextMeshProUGUI>().text.Where(c => char.IsDigit(c)).ToArray());
            bool parse_success = int.TryParse(id_string, out part_id);


            return part_id;
        }
    }
}