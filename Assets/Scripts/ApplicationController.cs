
using Assets.Scripts.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Requests;
using Assets.Scripts.Model;

namespace Assets.Scripts
{

    public class ApplicationController : Singleton<ApplicationController>
    {

        DataManager dataManager;
        MenuCanvasManager canvasManager;
        ApplicationView applicationView;

        private int curr_identifying_participant;

        protected override void Awake()
        {
            base.Awake();
            curr_identifying_participant = 0;
            dataManager = DataManager.GetInstance();
            canvasManager = MenuCanvasManager.GetInstance();
            applicationView = ApplicationView.GetInstance();
            Debug.Log("App controller Awake");
        }


        public void OnExperimentIdEnterStart()
        {
            Debug.Log("In OnExperimentIdEnterStart");
            applicationView.OpenScreen(ScreenType.ExperimentIdEnteringMenu);
        }

        public void OnExperimentIdEnterSend()
        {
            //todo
            ExperimentIdCanvasController experimentIdCanvasController = canvasManager.GetExperimentIdCanvasController();
            string experimentId = experimentIdCanvasController.GetExperimentId();

            applicationView.OpenScreen(ScreenType.MainMenu);
        }

        public void StartFirstParticipantIdentification()
        {
            curr_identifying_participant = 1;
            applicationView.OpenScreen(ScreenType.ParticipantIdentificationTypeChoiceMenu);
        }

        public void StartSecondParticipantIdentification()
        {
            curr_identifying_participant = 2;
            applicationView.OpenScreen(ScreenType.ParticipantIdentificationTypeChoiceMenu);
        }

        public void OnRegistrationChosen()
        {
            applicationView.OpenScreen(ScreenType.ParticipantRegistrationMenu);
        }
        public void OnAuthorisationChosen()
        {
            applicationView.OpenScreen(ScreenType.ParticipantAuthorisationMenu);
        }


        public void OnRegistrationSend()
        {
            
            ParticipantRegistrationCanvasController registrationCanvasController = canvasManager.GetParticipantRegistrationCanvasController();
            RegistrationRequest registrationInfo = registrationCanvasController.GetRegistrationInfo();
            int res = dataManager.Register(registrationInfo);
            if (res != 0)
            {
                //todo
            }

            //todo сделать нотификейшн с выданным id, чтобы чел точно его записал где-нибудь
            //todo Добавить в эксперимент манагер инфу про партисипанта

            applicationView.OpenScreen(ScreenType.ParticipantInExperimentMenu);
        }

        public void OnAuthorisationSend()
        {
            ParticipantAuthorisationCanvasController authorisationCanvasController = canvasManager.GetParticipantAuthorisationCanvasController();
            AuthorisationRequest authorisationInfo = authorisationCanvasController.GetAuthorisationInfo();



            int res = dataManager.Login(authorisationInfo);
            if (res != 0)
            {
                //todo
            }

            //todo Добавить в эксперимент манагер инфу про партисипанта

            applicationView.OpenScreen(ScreenType.ParticipantInExperimentMenu);
        }
        
        public void OnParticipantInExperimentSend()
        {
            ParticipantInExperimentCanvasController participantInExperimentCanvasController = canvasManager.GetParticipantInExperimentCanvasController();
            ParticipantInExperiment participantInExperiment = participantInExperimentCanvasController.GetParticipantInExperiment();

            //todo сделать сохранение этой инфы в эксперимент манагере

            applicationView.OpenScreen(ScreenType.MainMenu);
        }

        public void OnBaseAlphaStart()
        {
           System.Diagnostics.Process myProcess = new System.Diagnostics.Process();

           myProcess.StartInfo.FileName = "F:\\TestModule.exe.lnk";
           myProcess.Start();

        }


        public void StartExperiment()
        {

        }

        public void OnCloseError()
        {
            applicationView.CloseNotificationMessage();
        }




    }
}