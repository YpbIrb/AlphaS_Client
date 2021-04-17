
using Assets.Scripts.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Utility;
using System;

namespace Assets.Scripts
{

    public class ApplicationController : Singleton<ApplicationController>
    {

        DataManager dataManager;
        MenuCanvasManager canvasManager;
        ApplicationView applicationView;

        protected override void Awake()
        {
            base.Awake();

            dataManager = DataManager.GetInstance();
            canvasManager = MenuCanvasManager.GetInstance();
            applicationView = ApplicationView.GetInstance();
            Debug.Log("App controller Awake");
        }


        public void OnNotification(Notification notification)
        {

            var res = 0;
            switch (notification)
            {
                case Notification.RegistrationChosen:
                    applicationView.OpenScreen(ScreenType.RegistrationMenu);
                    break;

                case Notification.RegistrationSend:
                    RegistrationCanvasController registrationCanvasController = canvasManager.GetRegistrationCanvasController();
                    RegistrationRequest registrationInfo = registrationCanvasController.GetRegistrationInfo();
                    res = dataManager.Register(registrationInfo);
                    if (res != 0)
                    {

                    }

                    //var net_manager = AlphaSNetManager.GetInstance();
                    //https://localhost:5001/api/Participant
                    //net_manager.SendGet("https://localhost:5001/api/Participant");

                    applicationView.OpenScreen(ScreenType.MainMenu);
                    break;

                case Notification.AuthorisationChosen:
                    applicationView.OpenScreen(ScreenType.AuthorisationMenu);
                    
                    break;

                case Notification.AuthorisationSend:
                    AuthorisationCanvasController authorisationCanvasController = canvasManager.GetAuthorisationCanvasController();
                    AuthorisationRequest authorisationInfo = authorisationCanvasController.GetAuthorisationInfo();

                    res = dataManager.Login(authorisationInfo);
                    if (res != 0)
                    {

                    }
                    applicationView.OpenScreen(ScreenType.MainMenu);
                    break;


                case Notification.MatchingStart:
                    Debug.Log("Opening matching screen");
                    applicationView.OpenScreen(ScreenType.MatchingScreen);
                    break;

                case Notification.BaseAlphaStart:
                    applicationView.ShowErrorMessage("No base alpha ((");

                    Debug.Log(":AOSDkfjs;dlfkjad;rlrk");

                    applicationView.OpenScreen(ScreenType.MainMenu);
                    break;

                case Notification.GameStart:
                    applicationView.OpenScreen(ScreenType.GameScreen);
                    break;

                case Notification.MatchingFinish:
                    applicationView.OpenScreen(ScreenType.MainMenu);
                    break;

                case Notification.CloseError:
                    applicationView.CloseErrorMessage();
                    break;

                default:
                    break;
            }
        }

        public void OnRegistrationChosen()
        {
            applicationView.OpenScreen(ScreenType.RegistrationMenu);
        }

        public void OnRegistrationSend()
        {
            RegistrationCanvasController registrationCanvasController = canvasManager.GetRegistrationCanvasController();
            RegistrationRequest registrationInfo = registrationCanvasController.GetRegistrationInfo();
            int res = dataManager.Register(registrationInfo);
            if (res != 0)
            {

            }

            //var net_manager = AlphaSNetManager.GetInstance();
            //https://localhost:5001/api/Participant
            //net_manager.SendGet("https://localhost:5001/api/Participant");

            applicationView.OpenScreen(ScreenType.MainMenu);
        }

        public void OnAuthorisationChosen()
        {
            applicationView.OpenScreen(ScreenType.AuthorisationMenu);
        }

        public void OnAuthorisationSend()
        {
            AuthorisationCanvasController authorisationCanvasController = canvasManager.GetAuthorisationCanvasController();
            AuthorisationRequest authorisationInfo = authorisationCanvasController.GetAuthorisationInfo();

            int res = dataManager.Login(authorisationInfo);
            if (res != 0)
            {

            }
            applicationView.OpenScreen(ScreenType.MainMenu);
        }

        public void OnMatchingStart()
        {
            Debug.Log("Opening matching screen");
            applicationView.OpenScreen(ScreenType.MatchingScreen);
        }

        public void OnBaseAlphaStart()
        {
            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
                
           //myProcess.StartInfo.UseShellExecute = false;
           //myProcess.StartInfo.FileName = "F:\\Unity projects\\TestModuleBuild\\TestModule.exe";
           myProcess.StartInfo.FileName = "F:\\TestModule.exe.lnk";
           myProcess.Start();

            


            //applicationView.ShowErrorMessage("No base alpha ((");

            //Debug.Log(":AOSDkfjs;dlfkjad;rlrk");

            //applicationView.OpenScreen(ScreenType.MainMenu);
        }

        public void OnMatchingFinish()
        {
            applicationView.OpenScreen(ScreenType.MainMenu);
        }


        public void OnCloseError()
        {
            applicationView.CloseErrorMessage();
        }




    }
}