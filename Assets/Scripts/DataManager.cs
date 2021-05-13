using Assets.Scripts.Model;
using Assets.Scripts.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using UnityEngine;




namespace Assets.Scripts
{
    public class DataManager : Singleton<DataManager>
    {

        AlphaSNetManager netManager;


        protected override void Awake()
        {
            base.Awake();
            netManager = AlphaSNetManager.GetInstance();
        }

        /*
         * Возвращает партисипанта с id = 
         * -1 при проблемах с соединением с сервером, 
         * -2 при отсутствии доступа, 
         * -3 при неправильном адресе запроса,
         * -4 при другой ошибке сервера
         */
        public Participant Register(ParticipantRegistrationRequest registrationInfo)
        {
            string registration_json = JsonConvert.SerializeObject(registrationInfo);
            HttpResponseMessage response = netManager.SendRegistrationRequestAsync(registration_json);
            if(response == null)
            {
                UnityEngine.Debug.Log("Unseccessfull http GetParticipant request. Troubles with connection");
                return new Participant(-1);
            }

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                UnityEngine.Debug.Log("Get Participant from server (after registration): \n" + responseBody);
                var jpart = JObject.Parse(responseBody);
                Participant part = jpart.ToObject<Participant>();
                return part;
            }
            else
            {
                UnityEngine.Debug.Log("Unseccessfull http registration request. StatusCode : " + response.StatusCode);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new Participant(-2);

                    case System.Net.HttpStatusCode.NotFound:
                        return new Participant(-3);

                    default:
                        return new Participant(-4);
                }
            }

        }

        /*
         * Возвращает партисипанта с id = 
         * -1 при проблемах с соединением с сервером, 
         * -2 при отсутствии доступа, 
         * -3 если отсутствует испытуемый с таким id, 
         * -4, если другая ошибка сервера
         */
        public Participant Login(int id)
        {
            //string authprosation_json = JsonConvert.SerializeObject(authorisationInfo);
            HttpResponseMessage response = netManager.GetParticipantRequest(id);
            if(response == null)
            {
                UnityEngine.Debug.Log("Unseccessfull http GetParticipant request. Troubles with connection");
                return new Participant(-1);
            }


            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var jpart = JObject.Parse(responseBody);
                UnityEngine.Debug.Log("Get Participant from server: \n" + responseBody);
                Participant part = jpart.ToObject<Participant>();
                return part;
            }
            else
            {
                UnityEngine.Debug.Log("Unseccessfull http GetParticipant request. StatusCode : " + response.StatusCode);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new Participant(-2);

                    case System.Net.HttpStatusCode.NotFound:
                        return new Participant(-3);

                    default:
                        return new Participant(-4);
                }
            }
        }

        /*
         * Возвращает эксперимент с id =
         * -1 при проблемах с соединением с сервером, 
         * -2 при отсутствии доступа, 
         * -3 если отсутствует испытуемый с таким id, 
         * -4, если другая ошибка сервера
         */
        public Experiment GetExperimentById(int id)
        {
            HttpResponseMessage response = netManager.GetExperimentRequest(id);
            if(response == null)
            {
                UnityEngine.Debug.Log("Unseccessfull http GetExperimentById request. Troubles with connection");
                return new Experiment(-1);
            }


            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var jpart = JObject.Parse(responseBody);
                UnityEngine.Debug.Log("Get Experiment from server: \n" + responseBody);
                Experiment exp = jpart.ToObject<Experiment>();
                return exp;
            }
            else
            {
                UnityEngine.Debug.Log("Unseccessfull http GetExperimentById request. StatusCode : " + response.StatusCode);

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new Experiment(-2);


                    case System.Net.HttpStatusCode.NotFound:
                        return new Experiment(-3);

                    default:
                        return new Experiment(-4); ;
                }

            }
        }

        /*
         * Возвращает null при ошибке сервера
         */
        public List<Module> GetAllModules()
        {
            HttpResponseMessage response = netManager.GetAllModulesRequest();
            if(response == null)
            {
                UnityEngine.Debug.Log("Unseccessfull http GetAllModulesRequest request. Troubles with connection");
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                UnityEngine.Debug.Log("AllModules : responseBody " + responseBody);
                var jpart = JArray.Parse(responseBody);
                List<Module> modules = jpart.ToObject<List<Module>>();
                return modules;
            }
            else
            {
                UnityEngine.Debug.Log("Unseccessfull http GetAllModulesRequest request. StatusCode : " + response.StatusCode);
                return null;
            }

        }

        /*
         * Возвращает модуль с id = 
         * -1 при проблемах с соединением с сервером, 
         * -2 при отсутствии доступа, 
         * -3 если отсутствует испытуемый с таким id, 
         * -4, если другая ошибка сервера
         */
        public Module GetModuleByName(string moduleName)
        {
            HttpResponseMessage response = netManager.GetModuleRequest(moduleName);
            if (response == null)
            {
                UnityEngine.Debug.Log("Unseccessfull http GetParticipant request. Troubles with connection");
                return new Module(-1);
            }

            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                UnityEngine.Debug.Log("Module : responseBody" + responseBody);
                var jpart = JObject.Parse(responseBody);
                Module module = jpart.ToObject<Module>();
                return module;
            }
            else
            {
                UnityEngine.Debug.Log("Unseccessfull http GetModule request. StatusCode : " + response.StatusCode);

                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        return new Module(-2);


                    case System.Net.HttpStatusCode.NotFound:
                        return new Module(-3);

                    default:
                        return new Module(-4); ;

                }
            }

        }

        /*
         * Возвращает 0 при успехе, 
         * -1 при проблемах с соединением с сервером, 
         * -2 при отсутствии доступа, 
         * -3 при неправильном адресе запроса,
         * -4 при другой ошибке.
         */
        public int SendExperimentUpdate(Experiment experiment)
        {
            int id = experiment.ExperimentId;
            string experiment_json = JsonConvert.SerializeObject(experiment);
            Debug.Log(experiment_json);
            HttpResponseMessage response = netManager.SendExperimentUpdateRequest(id, experiment_json);
            if (response == null)
            {
                UnityEngine.Debug.Log("Unseccessfull http SendExpetimentUpdate request. Troubles with connection");
                return -1;
            }

            if (response.IsSuccessStatusCode)
            {
                return 0;
            }
            else
            {
                UnityEngine.Debug.Log("Unseccessfull http SendExpetimentUpdate request. StatusCode : " + response.StatusCode);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        return -2;

                    case System.Net.HttpStatusCode.NotFound:
                        return -3;

                    default:
                        return -4;
                }
            }
        }

        /*
         * Возвращает 0 при успехе, 
         * -1 при проблемах с соединением с сервером, 
         * -2 при отсутствии доступа, 
         * -3 при неправильном адресе запроса,
         * -4 при другой ошибке.
         */
        public int SendLogin(OperatorLoginRequest operatorLoginRequest)
        {
            string login_json = JsonConvert.SerializeObject(operatorLoginRequest);
            Debug.Log(login_json);

            HttpResponseMessage response = netManager.SendOperatorLoginRequest(login_json);
            if(response == null)
            {
                UnityEngine.Debug.Log("Unseccessfull http OperatorLogin request. Troubles with connection");
                return -1;
            }
            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                var jpart = JObject.Parse(responseBody);
                JToken jToken = jpart.GetValue("token");
                string token = jToken.ToString();
                return 0;
            }
            else
            {
                UnityEngine.Debug.Log("Unseccessfull http OperatorLogin request. StatusCode : " + response.StatusCode);
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.Unauthorized:
                        return -2;

                    case System.Net.HttpStatusCode.NotFound:
                        return -3;
                    default:
                        return -4;
                }
            }
        }
    }
}