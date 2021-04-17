using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Menu
{

    public enum ButtonType
    {
        IdentificationTypeChoice_Authorisation,
        IdentificationTypeChoice_Registration,
        Authorisation_Send,
        Registration_Send,
        Main_BaseAlpha_Start,
        Main_Matching_Start,
        Main_Game_Start,
        Error_close
    }


    [RequireComponent(typeof(Button))]
    public class ButtonController : MonoBehaviour
    {
        public ButtonType buttonType;

        public delegate void ButtonClickHandler();
        public event ButtonClickHandler Notify;

        //public CanvasController canvasController;
        ApplicationController applicationController;
        //MenuCanvasManager canvasManager;
        Button menuButton;

        private void Start()
        {
            menuButton = GetComponent<Button>();
            menuButton.onClick.AddListener(OnButtonClicked);
            //canvasManager = MenuCanvasManager.GetInstance();
            applicationController = ApplicationController.GetInstance();

            switch (buttonType)
            {
                case ButtonType.IdentificationTypeChoice_Authorisation:
                    Notify += applicationController.OnAuthorisationChosen;
                    break;

                case ButtonType.IdentificationTypeChoice_Registration:
                    Notify += applicationController.OnRegistrationChosen;
                    break;

                case ButtonType.Authorisation_Send:
                    Notify += applicationController.OnAuthorisationSend;
                    break;

                case ButtonType.Registration_Send:
                    Notify += applicationController.OnRegistrationSend;
                    break;


                case ButtonType.Main_Matching_Start:
                    Notify += applicationController.OnMatchingStart;
                    break;

                case ButtonType.Main_BaseAlpha_Start:
                    Notify += applicationController.OnBaseAlphaStart;
                    break;

                case ButtonType.Main_Game_Start:
                    //applicationController.OnNotification(Notification.GameStart);
                    break;

                case ButtonType.Error_close:
                    Notify += applicationController.OnCloseError;
                    break;

                default:
                    break;
            }


        }


        void OnButtonClicked()
        {
            Notify?.Invoke();
            /*
            switch (buttonType)
            {
                case ButtonType.IdentificationTypeChoice_Authorisation:
                    applicationController.OnNotification(Notification.AuthorisationChosen);
                    break;

                case ButtonType.IdentificationTypeChoice_Registration:
                    applicationController.OnNotification(Notification.RegistrationChosen);
                    break;

                case ButtonType.Authorisation_Send:
                    applicationController.OnNotification(Notification.AuthorisationSend);
                    break;

                case ButtonType.Registration_Send:
                    applicationController.OnNotification(Notification.RegistrationSend);
                    break;


                case ButtonType.Main_Assigment_Start:
                    applicationController.OnNotification(Notification.MatchingStart);
                    break;

                case ButtonType.Main_BaseAlpha_Start:
                    applicationController.OnNotification(Notification.BaseAlphaStart);
                    break;

                case ButtonType.Main_Game_Start:
                    applicationController.OnNotification(Notification.GameStart);
                    break;

                case ButtonType.Error_close:
                    applicationController.OnNotification(Notification.CloseError);
                    break;

                default:
                    break;
            }

            */
        }
    }
}