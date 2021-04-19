
using Assets.Scripts.Menu;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Assets.Scripts.Requests;
using Assets.Scripts.Model;
using System.Globalization;
using System.Linq;

namespace Assets.Scripts
{

    public class ApplicationController : Singleton<ApplicationController>
    {

        DataManager dataManager;
        MenuCanvasManager canvasManager;
        ApplicationView applicationView;
        ExperimentManager experimentManager;

        private int curr_identifying_participant;

        protected override void Awake()
        {
            base.Awake();
            curr_identifying_participant = 0;
            dataManager = DataManager.GetInstance();
            canvasManager = MenuCanvasManager.GetInstance();
            applicationView = ApplicationView.GetInstance();
            Debug.Log("App controller Awake");


            experimentManager = new ExperimentManager();
            experimentManager.AllModules = dataManager.GetAllModules(); ;
            Debug.Log("experimentManager.AllModules.Count = " + experimentManager.AllModules.Count);
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

            int experimentId_int;
            string experimentId_string = experimentIdCanvasController.GetExperimentId();
            experimentId_string = new string(experimentId_string.Where(c => char.IsLetter(c) || char.IsDigit(c)).ToArray());

            Debug.Log("Getting experiment with id = " + experimentId_string);


            //todo перести валидацию в канвас контроллер


            bool parse_success = int.TryParse(experimentId_string, out experimentId_int);

            if (!parse_success)
            {
                applicationView.ShowNotificationMessage("Неудалось распарсить Id эксперимента");
                applicationView.OpenScreen(ScreenType.ExperimentIdEnteringMenu);
            }

            Experiment experiment = dataManager.GetExperimentById(experimentId_int);
            
            experimentManager.experiment = experiment;

            Debug.Log(experiment.ToString());



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
            Participant res = dataManager.Register(registrationInfo);

            if (res != null)
            {
                //todo проверка пола, и если M, то выключать поле Period

                applicationView.ShowNotificationMessage("Ваш ID : " + res.ParticipantId);
                experimentManager.SetParticipantId(curr_identifying_participant, res.ParticipantId);
                applicationView.OpenScreen(ScreenType.ParticipantInExperimentMenu);
            }
            else
            {
                applicationView.ShowNotificationMessage("Неудачная попытка регистрации. Не удалось соединиться с сервером");
            }
            //todo сделать нотификейшн с выданным id, чтобы чел точно его записал где-нибудь
            //todo Добавить в эксперимент манагер инфу про партисипанта

            
        }

        public void OnAuthorisationSend()
        {
            ParticipantAuthorisationCanvasController authorisationCanvasController = canvasManager.GetParticipantAuthorisationCanvasController();
            int part_id = authorisationCanvasController.GetAuthorisationId();

            Participant res = dataManager.Login(part_id);
            if (res != null)
            {
                //todo проверка пола, и если M, то выключать поле Period
                experimentManager.SetParticipantId(curr_identifying_participant, res.ParticipantId);
                applicationView.OpenScreen(ScreenType.ParticipantInExperimentMenu);

                //todo
            }
            else
            {   if(res.ParticipantId == -1)
                {
                    applicationView.ShowNotificationMessage("Ошибка в авторизации по данному Id. Попробуете проверить правильность ввода, или зарегистрируйтесь");
                }
                else
                {
                    applicationView.ShowNotificationMessage("Проблемы при авторизации(не связаны именно с этим id)");
                }
            }

            //todo Добавить в эксперимент манагер инфу про партисипанта

        }
        
        public void OnParticipantInExperimentSend()
        {
            ParticipantInExperimentCanvasController participantInExperimentCanvasController = canvasManager.GetParticipantInExperimentCanvasController();
            ParticipantInExperiment participantInExperiment = participantInExperimentCanvasController.GetParticipantInExperiment();

            experimentManager.SetParticipantInExperimentInfo(curr_identifying_participant, participantInExperiment);

            //todo сделать сохранение этой инфы в эксперимент манагере

            applicationView.OpenScreen(ScreenType.MainMenu);
        }

        public void StartExperiment()
        {
            if (experimentManager != null && experimentManager.experiment != null)
            {
                Debug.Log("Starting ExecuteExperiment");
                applicationView.OpenScreen(ScreenType.ExperimentProcessMenu);
                experimentManager.StartExperiment();
            }
            else
            {
                applicationView.ShowNotificationMessage("No Experiment to start");
            }
        }

        public void ContinueExperiment()
        {
            //todo
            experimentManager.ContinueExperiment();
        }

        public void FinishExperiment()
        {
            //todo отправка данных, + чтобы в нотификации говорилось о том, куда данные сохранены(локально, или на сервачок)
            Experiment experiment = experimentManager.GetFinalExperimentInfo();

            int res = dataManager.SendExperimentUpdate(experiment);
            if (res == 0)
            {
                applicationView.ShowNotificationMessage("Эксперимент завершен. Данные успешно загружены на сервер.");
            }
            else
            {
                applicationView.ShowNotificationMessage("Эксперимент завершен. На сервер данные не загружены.");
            }
            
            applicationView.OpenScreen(ScreenType.MainMenu);
        }


        public void OnCloseError()
        {
            applicationView.CloseNotificationMessage();
        }




    }
}