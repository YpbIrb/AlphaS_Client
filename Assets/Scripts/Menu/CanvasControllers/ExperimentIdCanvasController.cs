using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class ExperimentIdCanvasController : CanvasController
    {

        [SerializeField]
        TMP_InputField IDInputField;

        [SerializeField]
        GameObject ValidationWarningText;

        [SerializeField]
        Button SendButton;

        protected void Awake()
        {
            Debug.Log("In Awake in ExperimentIdCanvasController");
            this.menuCanvasType = MenuCanvasType.ExperimentIdEnteringMenu;
            ValidationWarningText.SetActive(false);
            IDInputField.onEndEdit.AddListener(delegate { ValidateInput(); });
            SendButton.interactable = false;
        }

        public int GetExperimentId()
        {
            int exp_id = 0;
            string id_string = new string(IDInputField.text.Where(c => char.IsDigit(c)).ToArray());
            bool parse_success = int.TryParse(id_string, out exp_id);
            return exp_id;
        }

        public void ValidateInput()
        {
            int exp_id = 0;
            string id_string = new string(IDInputField.text.Where(c => char.IsDigit(c)).ToArray());
            bool parse_success = int.TryParse(id_string, out exp_id);
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