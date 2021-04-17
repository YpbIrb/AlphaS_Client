using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;




namespace Assets.Scripts.Menu
{
    public enum MenuCanvasType
    {
        ExperimentIdEnteringMenu,
        MainMenu,
        ParticipantIdentificationTypeChoiceMenu,
        ParticipantAuthorisationMenu,
        ParticipantRegistrationMenu,
        ParticipantInExperimentMenu,
        NotificationMessageMenu
    }



    public class MenuCanvasManager: Singleton<MenuCanvasManager>
    {

        List<CanvasController> canvasControllerList;
        CanvasController lastActiveCanvas;

        Canvas canvas;



        protected override void Awake()
        {
            base.Awake();
            canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
            canvas = GetComponentInChildren<Canvas>();
            canvasControllerList.ForEach(x => {
                Debug.Log("Deactivateing Canvas with type: " + x.menuCanvasType);
                x.gameObject.SetActive(false);
            });
            OpenCanvas(MenuCanvasType.MainMenu);
        }

        public void OpenCanvas(MenuCanvasType _type)
        {
            if (lastActiveCanvas != null)
            {
                lastActiveCanvas.gameObject.SetActive(false);
            }

            CanvasController desiredCanvas = canvasControllerList.Find(x => x.menuCanvasType == _type);
            if (desiredCanvas != null)
            {
                desiredCanvas.gameObject.SetActive(true);
                lastActiveCanvas = desiredCanvas;
            }
            else { Debug.LogWarning("The desired canvas was not found!"); }

            if (!canvas.enabled)
            {
                canvas.enabled = true;
            }

            Debug.Log("Opening Canvas . type = " + _type);

        }

        public CanvasController GetCanvasControllerByType(MenuCanvasType _type)
        {
            CanvasController desiredCanvas = canvasControllerList.Find(x => x.menuCanvasType == _type);
            if (desiredCanvas != null)
            {
                return desiredCanvas;
            }
            else
            {

                Debug.LogWarning("The desired canvas was not found!");
                return null;
            }
        }

        public ExperimentIdCanvasController GetExperimentIdCanvasController()
        {
            return (ExperimentIdCanvasController)GetCanvasControllerByType(MenuCanvasType.ExperimentIdEnteringMenu);
        }


        public ParticipantAuthorisationCanvasController GetParticipantAuthorisationCanvasController()
        {
            return (ParticipantAuthorisationCanvasController)GetCanvasControllerByType(MenuCanvasType.ParticipantAuthorisationMenu);
        }

        public ParticipantRegistrationCanvasController GetParticipantRegistrationCanvasController()
        {
            return (ParticipantRegistrationCanvasController)GetCanvasControllerByType(MenuCanvasType.ParticipantRegistrationMenu);
        }

        public ParticipantInExperimentCanvasController GetParticipantInExperimentCanvasController()
        {
            return (ParticipantInExperimentCanvasController)GetCanvasControllerByType(MenuCanvasType.ParticipantInExperimentMenu);
        }

        public NotificationCanvasController GetNotificationCanvasController()
        {
            return (NotificationCanvasController)GetCanvasControllerByType(MenuCanvasType.NotificationMessageMenu);
        }
        

        public void DisableMenu()
        {
            canvas.enabled = false;
        }

        public void EnableMenu()
        {
            canvas.enabled = true;
        }

        public void ShowNotification()
        {
            OpenCanvas(MenuCanvasType.NotificationMessageMenu);
        }

        public void CloseNotification()
        {

        }


    }
}