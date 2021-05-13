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
        GameObject PeriodsDropdownWithHeader;

        [SerializeField]
        TMP_InputField AdditionalInformationInputField;

        [SerializeField]
        TMP_Text Header;

        protected void Awake()
        {
            Debug.Log("In Awake in IdentificationTypeCanvasController");
            this.menuCanvasType = MenuCanvasType.ParticipantInExperimentMenu;
        }

        public ParticipantInExperiment GetParticipantInExperiment()
        {
            ParticipantInExperiment res = new ParticipantInExperiment();

            if (IntoxicationDropdown.GetComponent<TMP_Dropdown>().captionText.text == "Да")
                res.Intoxication = true;

            if (HeadInjuryDropdown.GetComponent<TMP_Dropdown>().captionText.text == "Да")
                res.HeadInjury = true;

            if (PeriodsDropdown.GetComponent<TMP_Dropdown>().captionText.text == "Да")
                res.Periods = true;

            res.AdditionalInfo = AdditionalInformationInputField.text;

            //todo
            return res;
        }

        public void HidePeriod()
        {
            PeriodsDropdownWithHeader.SetActive(false);
        }

        public void ShowPeriod()
        {
            PeriodsDropdownWithHeader.SetActive(true);
        }

        public void SetParticipantId(int id)
        {

            Header.text = "Испытуемый с id = " + id;
        }


    }
}