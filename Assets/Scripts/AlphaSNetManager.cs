using Assets.Scripts.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Sockets;
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
        private const string participant_get_url = "/Participants/";
        private const string operator_login_url = "/Accounts/login/";

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

        public HttpResponseMessage SendRegistrationRequestAsync(string registrationRequest)
        {
            UnityEngine.Debug.Log("Sending registration request. Url : " + base_url + participant_creation_url);
            StringContent content = new StringContent(registrationRequest, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = client.PostAsync(base_url + participant_creation_url, content).Result;
                return response;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e.Message);
                return null;
            }
        }

        public HttpResponseMessage GetModuleRequest(string moduleName)
        {
            UnityEngine.Debug.Log("Sending GetModule request, Url : " + base_url + module_get_url + moduleName);
            
            try
            {
                HttpResponseMessage response = client.GetAsync(base_url + module_get_url + moduleName).Result;
                return response;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e.Message);
                return null;
            }
        }

        public HttpResponseMessage GetAllModulesRequest()
        {
            UnityEngine.Debug.Log("Sending GetAllModulesRequest request");
            UnityEngine.Debug.Log("Sending GetAllModulesRequest request, Url : " + base_url + allModules_get_url);
            try
            {
                HttpResponseMessage response = client.GetAsync(base_url + allModules_get_url).Result;
                return response;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e.Message);
                return null;
            }
        }

        public HttpResponseMessage GetExperimentRequest(int id)
        {
            UnityEngine.Debug.Log("Sending GetExperiment request, Url : " + base_url + experiment_get_url + id);
            try
            {
                HttpResponseMessage response = client.GetAsync(base_url + experiment_get_url + id).Result;
                return response;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e.Message);
                return null;
            }
        }

        public HttpResponseMessage GetParticipantRequest(int id)
        {
            UnityEngine.Debug.Log("Sending GetParticipant request, Url : " + base_url + participant_get_url + id);
            try
            {
                HttpResponseMessage response = client.GetAsync(base_url + participant_get_url + id).Result;
                return response;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e.Message);
                return null;
            }
        }

        public HttpResponseMessage SendExperimentUpdateRequest(int id, string registrationRequest)
        {
            UnityEngine.Debug.Log("Sending ExperimentUpdate request, Url : " + base_url + experiment_update_url + id);
            StringContent content = new StringContent(registrationRequest, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = client.PostAsync(base_url + experiment_update_url + id, content).Result;
                return response;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e.Message);
                return null;
            }
        }

        public HttpResponseMessage SendOperatorLoginRequest(string loginRequest)
        {
            UnityEngine.Debug.Log("Sending OperatorLogin request, Url : " + base_url + operator_login_url);
            //StringContent content = new StringContent(ModuleRequest, Encoding.UTF8, "application/json");

            StringContent content = new StringContent(loginRequest, Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = client.PostAsync(base_url + operator_login_url, content).Result;

                if (response.IsSuccessStatusCode)
                {

                    string responseBody = response.Content.ReadAsStringAsync().Result;
                    UnityEngine.Debug.Log("Login : responseBody" + responseBody);
                    var jpart = JObject.Parse(responseBody);
                    JToken jToken = jpart.GetValue("token");
                    string token = jToken.ToString();
                    UnityEngine.Debug.Log("token string : " + token);

                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                }

                return response;
            }
            catch (Exception e)
            {
                UnityEngine.Debug.Log(e.Message);
                return null;
            }
            

        }
        


    }
}
