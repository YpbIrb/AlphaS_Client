
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
            experimentManager.AllModules = dataManager.GetAllModules();
            if(experimentManager.AllModules == null)
            {
                Debug.Log("Ошибка в начальной загрузке информации про все модули");
            }
            else
            {
                Debug.Log("Информация про все модули успешно загружена");
            }  
        }

        public void OnExperimentIdEnterStart()
        {
            Debug.Log("In OnExperimentIdEnterStart");
            applicationView.OpenScreen(ScreenType.ExperimentIdEnteringMenu);
        }

        public void OnExperimentIdEnterSend()
        {
            ExperimentIdCanvasController experimentIdCanvasController = canvasManager.GetExperimentIdCanvasController();

            int experiment_id = experimentIdCanvasController.GetExperimentId(); ;
            Experiment experiment = dataManager.GetExperimentById(experiment_id);

            switch (experiment.ExperimentId)
            {
                case -1:
                    applicationView.ShowNotificationMessage("Ошибки в соединении с сервером. Обращайтесь к администратору системы.");
                    applicationView.OpenScreen(ScreenType.MainMenu);
                    break;
                case -2:
                    applicationView.ShowNotificationMessage("Отсутствуют права доступа для получения информации про эксперимент.");
                    applicationView.OpenScreen(ScreenType.MainMenu);
                    break;
                case -3:
                    applicationView.ShowNotificationMessage("Отсутствует эксперимент с таким id. Проверьте правильность введенного id.");
                    break;
                case -4:
                    applicationView.ShowNotificationMessage("Ошибка на стороне сервера при попытке получить информацию об эксперименте. Обращайтесь к администратору.");
                    applicationView.OpenScreen(ScreenType.MainMenu);
                    break;
                default:
                    experimentManager.experiment = experiment;

                    //todo проверка наличия всех исполняемых файлов
                    List<string> missingExecs = experimentManager.GetMissingExecs();
                    if (missingExecs.Count != 0)
                    {
                        string notification_str = "Успешно получена информация про эксперимент с id = " + experiment.ExperimentId +
                            ". Отсутствуют явлыки следующих исполняемых файлов: \n";
                        foreach(string str in missingExecs)
                        {
                            notification_str += str + "\n";
                        }
                        notification_str += "Перед запуском эксперимента добавьте недостающие ярлыки в папку " + ExperimentManager.base_module_path;
                        applicationView.ShowNotificationMessage(notification_str);
                    }
                    else
                    {
                        applicationView.ShowNotificationMessage("Успешно получена информация про эксперимент с id = " + experiment.ExperimentId + 
                            ". Исполняемые файлы всех модулей присутствуют");
                        applicationView.OpenScreen(ScreenType.MainMenu);
                    }

                    
                    break;
            }


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
            ParticipantRegistrationRequest registrationInfo = registrationCanvasController.GetRegistrationInfo();
            Participant res = dataManager.Register(registrationInfo);

            switch (res.ParticipantId)
            {
                case -1:
                    applicationView.ShowNotificationMessage("Неудачная попытка регистрации испытуемого. Отсутствует соединение с сервером. Обращайтесь к администратору системы.");
                    break;
                case -2:
                    applicationView.ShowNotificationMessage("Неудачная попытка регистрации испытуемого. Недостаточно прав. Обращайтесь к администратору системы.");
                    break;
                case -3:
                case -4:
                    applicationView.ShowNotificationMessage("Неудачная попытка регистрации испытуемого. Внутренняя ошибка сервера. Обращайтесь к администратору системы.");
                    break;
                default:
                    if (!res.IsFemale())
                        canvasManager.GetParticipantInExperimentCanvasController().HidePeriod();
                    else
                        canvasManager.GetParticipantInExperimentCanvasController().ShowPeriod();

                    canvasManager.GetParticipantInExperimentCanvasController().SetParticipantId(res.ParticipantId);
                    applicationView.ShowNotificationMessage("Ваш ID : " + res.ParticipantId + ". Запишите его, чтобы, при повторной работе с системой, авторизоваться с его помощью.");
                    experimentManager.SetParticipantId(curr_identifying_participant, res.ParticipantId);
                    applicationView.OpenScreen(ScreenType.ParticipantInExperimentMenu);
                    break;
            }
        
        }

        public void OnAuthorisationSend()
        {
            ParticipantAuthorisationCanvasController authorisationCanvasController = canvasManager.GetParticipantAuthorisationCanvasController();
            int part_id = authorisationCanvasController.GetAuthorisationId();

            Participant res = dataManager.Login(part_id);

            switch (res.ParticipantId)
            {
                case -1:
                    applicationView.ShowNotificationMessage("Ошибки в соединении с сервером. Обращайтесь к администратору системы." +
                        "Подтвердить существование пользователя с данным Id не получилось. Дальнейшая работа предполагает, что id было введено верно.");
                    applicationView.OpenScreen(ScreenType.MainMenu);
                    experimentManager.SetParticipantId(curr_identifying_participant, part_id);
                    canvasManager.GetParticipantInExperimentCanvasController().ShowPeriod();
                    canvasManager.GetParticipantInExperimentCanvasController().SetParticipantId(part_id);
                    applicationView.OpenScreen(ScreenType.ParticipantInExperimentMenu);
                    break;
                case -2:
                    applicationView.ShowNotificationMessage("Отсутствуют права доступа для выполнения операции. Обращайтесь к администратору." +
                        "Подтвердить существование пользователя с данным Id не получилось. Дальнейшая работа предполагает, что id было введено верно.");
                    applicationView.OpenScreen(ScreenType.MainMenu);
                    experimentManager.SetParticipantId(curr_identifying_participant, part_id);
                    canvasManager.GetParticipantInExperimentCanvasController().ShowPeriod();
                    canvasManager.GetParticipantInExperimentCanvasController().SetParticipantId(part_id);
                    applicationView.OpenScreen(ScreenType.ParticipantInExperimentMenu);
                    break;
                case -3:
                    applicationView.ShowNotificationMessage("Отсутствует испытуемый с таким id. Проверьте правильность введенного id, либо зарегистрируйте нового испытуемого.");
                    applicationView.OpenScreen(ScreenType.ParticipantIdentificationTypeChoiceMenu);
                    break;
                case -4:
                    applicationView.ShowNotificationMessage("Ошибка на стороне сервера при попытке получить информацию про испытуемого. " +
                        "Подтвердить существование пользователя с данным Id не получилось. Дальнейшая работа предполагает, что id было введено верно.");
                    applicationView.OpenScreen(ScreenType.MainMenu);
                    experimentManager.SetParticipantId(curr_identifying_participant, part_id);
                    canvasManager.GetParticipantInExperimentCanvasController().ShowPeriod();
                    canvasManager.GetParticipantInExperimentCanvasController().SetParticipantId(part_id);
                    applicationView.OpenScreen(ScreenType.ParticipantInExperimentMenu);
                    break;
                default:
                    experimentManager.SetParticipantId(curr_identifying_participant, res.ParticipantId);
                    canvasManager.GetParticipantInExperimentCanvasController().SetParticipantId(res.ParticipantId);
                    if (!res.IsFemale())
                        canvasManager.GetParticipantInExperimentCanvasController().HidePeriod();
                    else
                        canvasManager.GetParticipantInExperimentCanvasController().ShowPeriod();
                    applicationView.OpenScreen(ScreenType.ParticipantInExperimentMenu);
                    break;
            }
        }
        
        public void OnParticipantInExperimentSend()
        {
            ParticipantInExperimentCanvasController participantInExperimentCanvasController = canvasManager.GetParticipantInExperimentCanvasController();
            ParticipantInExperiment participantInExperiment = participantInExperimentCanvasController.GetParticipantInExperiment();

            experimentManager.SetParticipantInExperimentInfo(curr_identifying_participant, participantInExperiment);

            if(curr_identifying_participant == 1)
            {
                applicationView.ShowNotificationMessage("Первый участник успешно добавлен.");
            }
            else
            {
                applicationView.ShowNotificationMessage("Второй участник успешно добавлен.");
            }
            

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
                applicationView.ShowNotificationMessage("Не выбран эксперимент");
                applicationView.OpenScreen(ScreenType.MainMenu);
            }
        }

        public void ContinueExperiment()
        {
            //todo
            experimentManager.ContinueExperiment();
        }

        public void FinishExperiment()
        {
            Experiment experiment = experimentManager.GetFinalExperimentInfo();

            int res = dataManager.SendExperimentUpdate(experiment);
            switch (res)
            {
                case 0:
                    applicationView.ShowNotificationMessage("Эксперимент завершен. Данные успешно загружены на сервер.");
                    break;
                default:
                    applicationView.ShowNotificationMessage("Эксперимент завершен. Ошибка в связи с сервером, данные на сервер не загружены. " +
                    "Повторная попытка загрузки будет при следующем включении приложения.");
                    dataManager.SaveExperimentLocally(experiment);
                    //todo локальное сохранение
                    break;
            }
            applicationView.OpenScreen(ScreenType.MainMenu);

        }


        public void OnCloseNotification()
        {
            applicationView.CloseNotificationMessage();
        }

        public void OnOperatorLoginSend()
        {
            OperatorLoginCanvasController operatorLoginCanvasController = canvasManager.GetOperatorLoginCanvasController();
            OperatorLoginRequest operatorLoginRequest = operatorLoginCanvasController.GetOperatorLoginRequest();

            int res = dataManager.SendLogin(operatorLoginRequest);
            switch (res)
            {
                case 0:
                    applicationView.ShowNotificationMessage("Авторизация успешна.");
                    dataManager.UploadLocalExperimentFiles();
                    applicationView.OpenScreen(ScreenType.MainMenu);
                    break;
                case -1:
                    applicationView.ShowNotificationMessage("Неудачная попытка авторизации. Ошибки в соединении с сервером. Обращайтесь к администратору системы.");
                    break;
                case -2:
                    applicationView.ShowNotificationMessage("Неудачная попытка авторизации. Неверные логин или пароль");
                    break;
                default:
                    applicationView.ShowNotificationMessage("Неудачная попытка авторизации. Ошибка на стороне сервера. Обращайтесь к администратору.");
                    break;
            }

            //applicationView.OpenScreen(ScreenType.MainMenu);
        }


    }
}