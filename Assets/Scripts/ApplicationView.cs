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
        ScreenType CurrentScreen;
        bool shows_error;

        override protected void Awake()
        {
            base.Awake();
            menuCanvasManager = MenuCanvasManager.GetInstance();
            shows_error = false;
        }

        public void ShowNotificationMessage(String message)
        {
            NextScreen = CurrentScreen;
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
                    CurrentScreen = ScreenType.OperatorLoginMenu;
                    break;

                case ScreenType.MainMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.MainMenu);
                    CurrentScreen = ScreenType.MainMenu;
                    break;

                case ScreenType.ExperimentIdEnteringMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.ExperimentIdEnteringMenu);
                    CurrentScreen = ScreenType.ExperimentIdEnteringMenu;
                    break;

                case ScreenType.ParticipantIdentificationTypeChoiceMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.ParticipantIdentificationTypeChoiceMenu);
                    CurrentScreen = ScreenType.ParticipantIdentificationTypeChoiceMenu;
                    break;

                case ScreenType.ParticipantAuthorisationMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.ParticipantAuthorisationMenu);
                    CurrentScreen = ScreenType.ParticipantAuthorisationMenu;
                    break;

                case ScreenType.ParticipantRegistrationMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.ParticipantRegistrationMenu);
                    CurrentScreen = ScreenType.ParticipantRegistrationMenu;
                    break;

                case ScreenType.ParticipantInExperimentMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.ParticipantInExperimentMenu);
                    CurrentScreen = ScreenType.ParticipantInExperimentMenu;
                    break;

                case ScreenType.NotificationScreen:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.NotificationMessageMenu);
                    CurrentScreen = ScreenType.NotificationScreen;
                    break;


                case ScreenType.ExperimentProcessMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.ExperimentProcessMenu);
                    CurrentScreen = ScreenType.ExperimentProcessMenu;
                    break;
            }
        }

    }
}
