using Assets.Scripts.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{

    enum ScreenType 
    {
        OperatorLoginMenu,
        ParticipantIdentificationTypeChoiceMenu,
        ParticipantRegistrationMenu,
        ParticipantAuthorisationMenu,
        ParticipantInExperimentMenu,
        ExperimentIdEnteringMenu,
        MainMenu,
        NotificationScreen,
        ExperimentProcessMenu
    }

    class ApplicationView : Singleton<ApplicationView>
    {

        MenuCanvasManager menuCanvasManager;
        ScreenType NextScreen;
        bool shows_error;

        override protected void Awake()
        {
            base.Awake();
            menuCanvasManager = MenuCanvasManager.GetInstance();
            shows_error = false;
        }

        public void ShowNotificationMessage(String message)
        {
            OpenScreenByType(ScreenType.NotificationScreen);
            menuCanvasManager.GetNotificationCanvasController().SetNotificationMessage(message);
            shows_error = true;

        }

        public void CloseNotificationMessage()
        {
            OpenScreenByType(NextScreen);
            shows_error = false;
        }

        public void OpenScreen(ScreenType screenType)
        {
            if (shows_error)
            {
                NextScreen = screenType;
            }
            else
            {
                OpenScreenByType(screenType);
            }
            
        }

        private void OpenScreenByType(ScreenType screenType)
        {
            switch (screenType)
            {
                case ScreenType.OperatorLoginMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.OperatorLoginMenu);
                    break;

                case ScreenType.MainMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.MainMenu);
                    break;

                case ScreenType.ExperimentIdEnteringMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.ExperimentIdEnteringMenu);
                    break;

                case ScreenType.ParticipantIdentificationTypeChoiceMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.ParticipantIdentificationTypeChoiceMenu);
                    break;

                case ScreenType.ParticipantAuthorisationMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.ParticipantAuthorisationMenu);
                    break;

                case ScreenType.ParticipantRegistrationMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.ParticipantRegistrationMenu);
                    break;

                case ScreenType.ParticipantInExperimentMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.ParticipantInExperimentMenu);
                    break;

                case ScreenType.NotificationScreen:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.NotificationMessageMenu);
                    break;


                case ScreenType.ExperimentProcessMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.ExperimentProcessMenu);
                    break;
            }
        }


        private void SwitchScene()
        {

        }


    }
}
