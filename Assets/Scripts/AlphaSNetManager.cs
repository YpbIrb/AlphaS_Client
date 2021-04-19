using Assets.Scripts.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    class AlphaSNetManager : Singleton<AlphaSNetManager>
    {
        private const string base_url = "http://localhost:8000/api";
        private const string participant_creation_url = "/Participants/Create";

        private const string experiment_get_url = "/Experiments/";
        private const string experiment_update_url = "/Experiments/Update/";
        private const string module_get_url = "/Modules/";
        private const string allModules_get_url = "/Modules";
        static HttpClient client;


        protected override void Awake()
        {
            base.Awake();
            client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }


        void SendGet(string url)
        {
            //string url = "https://localhost:5001/api/Participant";
            StartCoroutine(GetRequest(url));
        }

        void SendPost(string url, string json)
        {
            StartCoroutine(PostRequest(url, json));
        }


        IEnumerator GetRequest(string uri)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                UnityEngine.Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                UnityEngine.Debug.Log("Received: " + uwr.downloadHandler.text);
            }
        }

        IEnumerator PostRequest(string url, string json)
        {
            var uwr = new UnityWebRequest(url, "POST");
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
            uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");

            //Send the request then wait here until it returns
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                UnityEngine.Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                UnityEngine.Debug.Log("Received: " + uwr.downloadHandler.text);
            }
        }

        public Participant SendRegistrationRequestAsync(string registrationRequest)
        {
            UnityEngine.Debug.Log("Sending registration request. Url : " + base_url + participant_creation_url);
            StringContent content = new StringContent(registrationRequest, Encoding.UTF8, "application/json");
            using (HttpResponseMessage response = client.PostAsync(base_url + participant_creation_url, content).Result)
            {
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    var jpart = JObject.Parse(responseBody);
                    Participant part = jpart.ToObject<Participant>();
                    return part;
                }

                else
                {
                    UnityEngine.Debug.Log("Unseccessfull http registration request. StatusCode : " + response.StatusCode);
                    return null;
                }
            };
        }

        public Module GetModuleRequest(string moduleName)
        {
            //StringContent content = new StringContent(ModuleRequest, Encoding.UTF8, "application/json");
            UnityEngine.Debug.Log("Sending GetModule request, Url : " + base_url + module_get_url + moduleName);
            HttpResponseMessage response = client.GetAsync(base_url + module_get_url + moduleName).Result;
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
                return null;
            }
            
        }

        public List<Module> GetAllModulesRequest()
        {
            //todo
            UnityEngine.Debug.Log("Sending GetAllModulesRequest request");
            //StringContent content = new StringContent(ModuleRequest, Encoding.UTF8, "application/json");
            UnityEngine.Debug.Log("Sending GetAllModulesRequest request, Url : " + base_url + allModules_get_url);
            HttpResponseMessage response = client.GetAsync(base_url + allModules_get_url).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseBody = response.Content.ReadAsStringAsync().Result;
                UnityEngine.Debug.Log("AllModules : responseBody " + responseBody);
                var jpart = JArray.Parse(responseBody);
                List<Module> module = jpart.ToObject<List<Module>>();
                return module;
            }
            else
            {
                UnityEngine.Debug.Log("Unseccessfull http GetAllModulesRequest request. StatusCode : " + response.StatusCode);
                return null;
            }

        }

        public Experiment GetExperimentRequest(int id)
        {
            //todo
            UnityEngine.Debug.Log("Sending GetExperiment request, Url : " + base_url + experiment_get_url + id);
            //StringContent content = new StringContent(ModuleRequest, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.GetAsync(base_url + experiment_get_url + id).Result;
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
                UnityEngine.Debug.Log("Unseccessfull http GetExperiment request. StatusCode : " + response.StatusCode);
                return null;
            }
        }



    }
}
