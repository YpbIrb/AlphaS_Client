using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Assets.Scripts.Menu
{

    public enum ButtonType
    {
        ParticipantIdentificationTypeChoice_Authorisation,
        ParticipantIdentificationTypeChoice_Registration,
        ParticipantAuthorisation_Send,
        ParticipantRegistration_Send,
        FirstParticipantIdentification_Start,
        SecondParticipantIdentification_Start,
        ParticipantInExperiment_Send,
        ExperimentIdEnter_Start,
        ExperimentIdEnter_Send,
        Experiment_Start,
        Notification_close
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
                case ButtonType.ExperimentIdEnter_Start:
                    Notify += applicationController.OnExperimentIdEnterStart;
                    break;

                case ButtonType.ExperimentIdEnter_Send:
                    Notify += applicationController.OnExperimentIdEnterSend;
                    break;

                case ButtonType.FirstParticipantIdentification_Start:
                    Notify += applicationController.StartFirstParticipantIdentification;
                    break;

                case ButtonType.SecondParticipantIdentification_Start:
                    Notify += applicationController.StartSecondParticipantIdentification;
                    break;

                case ButtonType.ParticipantIdentificationTypeChoice_Registration:
                    Notify += applicationController.OnRegistrationChosen;
                    break;

                case ButtonType.ParticipantIdentificationTypeChoice_Authorisation:
                    Notify += applicationController.OnAuthorisationChosen;
                    break;

                case ButtonType.ParticipantRegistration_Send:
                    Notify += applicationController.OnRegistrationSend;
                    break;

                case ButtonType.ParticipantAuthorisation_Send:
                    Notify += applicationController.OnAuthorisationSend;
                    break;

                case ButtonType.ParticipantInExperiment_Send:
                    Notify += applicationController.OnParticipantInExperimentSend;
                    break;

                case ButtonType.Experiment_Start:
                    Notify += applicationController.StartExperiment;
                    break;

                case ButtonType.Notification_close:
                    Notify += applicationController.OnCloseError;
                    break;

                default:
                    break;
            }


        }


        void OnButtonClicked()
        {
            Notify?.Invoke();
        }
    }
}