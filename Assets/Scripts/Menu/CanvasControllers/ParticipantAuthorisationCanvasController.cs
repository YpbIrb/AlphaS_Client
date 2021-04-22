using Assets.Scripts.Requests;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{

    public class ParticipantAuthorisationCanvasController : CanvasController
    {
        [SerializeField]
        TMP_InputField IDInputField;

        [SerializeField]
        GameObject ValidationWarningText;

        [SerializeField]
        Button SendButton;

        protected void Awake()
        {
            Debug.Log("In Awake in AuthorisationCanvasController");
            this.menuCanvasType = MenuCanvasType.ParticipantAuthorisationMenu;
            SendButton.interactable = false;
            IDInputField.onEndEdit.AddListener(delegate { ValidateInput(); });
        }

        public AuthorisationRequest GetAuthorisationInfo()
        {
            return new AuthorisationRequest(IDInputField.text);
        }

        public int GetAuthorisationId()
        {

            int part_id = 0;
            string id_string = new string(IDInputField.text.Where(c => char.IsDigit(c)).ToArray());
            bool parse_success = int.TryParse(id_string, out part_id);

            return part_id;
        }

        public void ValidateInput()
        {
            int part_id = 0;
            string id_string = new string(IDInputField.text.Where(c => char.IsDigit(c)).ToArray());
            bool parse_success = int.TryParse(id_string, out part_id);
            if (parse_success)
            {
                SendButton.interactable = true;
                ValidationWarningText.SetActive(false);
            }
            else
            {
                SendButton.interactable = false;
                ValidationWarningText.SetActive(true);
            }

        }


    }
}