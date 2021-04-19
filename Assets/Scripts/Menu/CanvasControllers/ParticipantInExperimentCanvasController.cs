using Assets.Scripts.Menu;
using Assets.Scripts.Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;



namespace Assets.Scripts.Menu
{

    public class ParticipantInExperimentCanvasController : CanvasController
    {

        [SerializeField]
        GameObject IntoxicationDropdown;

        [SerializeField]
        GameObject HeadInjuryDropdown;

        [SerializeField]
        GameObject PeriodsDropdown;

        [SerializeField]
        TMP_InputField AdditionalInformationInputField;

        protected void Awake()
        {
            Debug.Log("In Awake in IdentificationTypeCanvasController");
            this.menuCanvasType = MenuCanvasType.ParticipantInExperimentMenu;
        }

        public ParticipantInExperiment GetParticipantInExperiment()
        {
            ParticipantInExperiment res = new ParticipantInExperiment();

            if (IntoxicationDropdown.GetComponent<TMP_Dropdown>().captionText.text == "Yes")
                res.Intoxication = true;

            if (HeadInjuryDropdown.GetComponent<TMP_Dropdown>().captionText.text == "Yes")
                res.HeadInjury = true;

            if (PeriodsDropdown.GetComponent<TMP_Dropdown>().captionText.text == "Yes")
                res.Periods = true;

            res.AdditionalInfo = AdditionalInformationInputField.text;

            //todo
            return res;
        }

    }
}