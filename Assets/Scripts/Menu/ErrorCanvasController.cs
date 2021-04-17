using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Assets.Scripts.Menu
{

    public class ErrorCanvasController : CanvasController
    {

        TMP_Text Error_Text;

        private string ERROR_MESSAGE ;

        public void SetErrorMessage(string message)
        {
            ERROR_MESSAGE = message;

            if (Error_Text == null)
            {
                Debug.Log("Error_Text = NULL");
            }


            Error_Text.text = ERROR_MESSAGE;
        }

        protected void Awake()
        {
            Debug.Log("In Awake in ErrorMessageMenu");
            this.menuCanvasType = MenuCanvasType.ErrorMessageMenu;
            ERROR_MESSAGE = "Default error message";
            Error_Text = GetComponentInChildren<TMPro.TMP_Text>();
            Error_Text.text = ERROR_MESSAGE;


            if (Error_Text == null)
            {
                Debug.Log("Error_Text = NULL in awake");
            }
            else
            {
                Debug.Log("Error_Text NOT NULL in awake");
            }

        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }



}