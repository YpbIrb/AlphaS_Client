using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Requests;
using TMPro;
using System;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class ParticipantRegistrationCanvasController : CanvasController
    {
        [SerializeField]
        TMP_InputField BirthDayInputField;

        [SerializeField]
        TMP_InputField BirthMonthInputField;

        [SerializeField]
        TMP_InputField BirthYearInputField;

        [SerializeField]
        TMP_InputField BirthCountryInputField;

        [SerializeField]
        TMP_InputField BirthCityInputField;

        [SerializeField]
        TMP_Dropdown GenderDropdown;

        [SerializeField]
        GameObject DateWarning;

        [SerializeField]
        GameObject RegistrationButton;

        bool is_date_correct;


        protected void Awake()
        {
            RegistrationButton.GetComponent<Button>().interactable = false;
            DateWarning.SetActive(false);
            BirthDayInputField.onEndEdit.AddListener(delegate { DateValidation(); });
            BirthMonthInputField.onEndEdit.AddListener(delegate { DateValidation(); });
            BirthYearInputField.onEndEdit.AddListener(delegate { DateValidation(); });
            Debug.Log("In Awake in RegistrationCanvasController");
            this.menuCanvasType = MenuCanvasType.ParticipantRegistrationMenu;
        }


        public RegistrationRequest GetRegistrationInfo()
        {

            DateTime birth_date = GetBirthDate();   

            RegistrationRequest res = new RegistrationRequest(GenderDropdown.GetComponent<TMP_Dropdown>().captionText.text, birth_date, BirthCityInputField.text, BirthCountryInputField.text) ;



            return res;
        }

        //Возвращает введенную пользователем дату, или null при ошибке конвертации
        private DateTime GetBirthDate()
        {
            int birth_day = Int32.Parse(BirthDayInputField.text);
            int birth_month = Int32.Parse(BirthMonthInputField.text);
            int birth_year = Int32.Parse(BirthYearInputField.text);

            DateTime res = new DateTime(birth_year, birth_month, birth_day);

            Debug.Log("Birth Date : " + res);
            return res;
        }

        public void DateValidation()
        {
            int birth_day;
            int birth_month;
            int birth_year;

            if (!Int32.TryParse(BirthDayInputField.text,    out birth_day)      ||
                !Int32.TryParse(BirthMonthInputField.text,  out birth_month)    ||
                !Int32.TryParse(BirthYearInputField.text,   out birth_year)       )
            {
                ShowDateError();
                RegistrationButton.GetComponent<Button>().interactable = false;
                is_date_correct = false;
                return;
            }

            try
            {
                DateTime res = new DateTime(birth_year, birth_month, birth_day);
                Debug.Log("In Validation. Birth Date : " + res);

                RegistrationButton.GetComponent<Button>().interactable = true;
                DateWarning.SetActive(false);
                is_date_correct = true;
            }
            catch (ArgumentOutOfRangeException)
            {   
                ShowDateError();
                RegistrationButton.GetComponent<Button>().interactable = false;
                is_date_correct = false;
                return;
            }

        }

        private void ShowDateError()
        {


        }

    }
}