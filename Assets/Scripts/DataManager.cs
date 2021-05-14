using Assets.Scripts.Model;
using Assets.Scripts.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using UnityEngine;




namespace Assets.Scripts
{
    public class DataManager : Singleton<DataManager>
    {

        AlphaSNetManager netManager;
        public const string experiments_path = "C:\\AlphaS\\Experiments\\";

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
                var jmodule = JObject.Parse(responseBody);
                Module module = jmodule.ToObject<Module>();
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

        public int SaveExperimentLocally(Experiment experiment)
        {
            Debug.Log("Saving experiment info locally");
            string file_path = experiments_path + experiment.StartTime.ToString(@"dd.mm.yyyy-HH/mm") + ".txt";
            FileStream fs = File.Create(file_path);
            string experiment_json = JsonConvert.SerializeObject(experiment);
            byte[] info = new UTF8Encoding(true).GetBytes(experiment_json);
            fs.Write(info, 0, info.Length);
            fs.Close();
            return 0;
        }

        public int UploadLocalExperimentFiles() 
        {
            List<string> filePaths = Directory.GetFiles(experiments_path).ToList();
            int error = 0;
            foreach(string path in filePaths)
            {
                StreamReader sr = File.OpenText(path);
                string experiment_json = "";
                string tmp = "";
                while ((tmp = sr.ReadLine()) != null)
                {
                    experiment_json += tmp;
                }
                sr.Close();
                try
                {
                    var jexp = JObject.Parse(experiment_json);
                    Experiment exp = jexp.ToObject<Experiment>();

                    HttpResponseMessage response = netManager.SendExperimentUpdateRequest(exp.ExperimentId, experiment_json);

                    if (response != null)
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            Debug.Log("Successfully uploaded experiment with id = " + exp.ExperimentId);
                            File.Delete(path);
                        }
                        else
                        {
                            Debug.Log("Error while uploading experiment with id = " + exp.ExperimentId);
                            Debug.Log("Responce code : " + response.StatusCode);
                            error = -1;
                        }
                    }
                    else
                    {
                        Debug.Log("Can't connect to server to upload experiment file ");
                        error = -1;
                    }
                }
                catch (JsonReaderException e)
                {
                    Debug.Log("Exception while parsing experiment file " + path );
                    Debug.Log(e.Message);
                    error = -1;
                }
            }


            return error;
        }

    }
}