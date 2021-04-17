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
        IdentificationTypeChoiceMenu,
        RegistrationMenu,
        AuthorisationMenu,
        MainMenu,
        BaseAlphaScreen,
        MatchingScreen,
        GameScreen,
        ErrorScreen
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
            Display.displays[1].Activate();
        }

        public void ShowErrorMessage(String message)
        {
            OpenScreenByType(ScreenType.ErrorScreen);
            menuCanvasManager.GetErrorCanvasController().SetErrorMessage(message);
            shows_error = true;

        }

        public void CloseErrorMessage()
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
                case ScreenType.IdentificationTypeChoiceMenu:
                    SceneManager.LoadScene(1, LoadSceneMode.Single);
                    break;

                case ScreenType.RegistrationMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.RegistrationMenu);
                    break;

                case ScreenType.MatchingScreen:
                    Debug.Log("Openeing MatchingScreen");
                    menuCanvasManager.DisableMenu();
                    SceneManager.LoadScene(2, LoadSceneMode.Single);
                    break;

                case ScreenType.AuthorisationMenu:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.AuthorisationMenu);
                    break;

                case ScreenType.MainMenu:
                    Debug.Log("Openeing Main Menu");
                    if (SceneManager.GetActiveScene().buildIndex != 0)
                        SceneManager.LoadScene(0, LoadSceneMode.Single);


                    menuCanvasManager.OpenCanvas(MenuCanvasType.MainMenu);
                    break;

                case ScreenType.BaseAlphaScreen:
                    SceneManager.LoadScene(1, LoadSceneMode.Single);
                    break;

                case ScreenType.GameScreen:
                    //SceneManager.LoadScene(3);
                    break;

                case ScreenType.ErrorScreen:
                    menuCanvasManager.OpenCanvas(MenuCanvasType.ErrorMessageMenu);
                    break;
            }
        }


        private void SwitchScene()
        {

        }


    }
}
