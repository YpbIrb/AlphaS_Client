using Assets.Scripts.Model;
using Assets.Scripts.Requests;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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


        public Participant Register(RegistrationRequest registrationInfo)
        {
            string registration_json = JsonConvert.SerializeObject(registrationInfo);
            Participant part = netManager.SendRegistrationRequestAsync(registration_json);
            if(part == null)
            {
                return part;
            }

            return part;
        }

        public Participant Login(int id)
        { 
            //string authprosation_json = JsonConvert.SerializeObject(authorisationInfo);
            Participant part = netManager.GetParticipantRequest(id);

            return part;
        }

        public Experiment GetExperimentById(int id)
        {
            Experiment exp = netManager.GetExperimentRequest(id);
            return exp;
        }  

        public List<Module> GetAllModules()
        {
            return netManager.GetAllModulesRequest();
        }

        public Module GetModuleByName(string moduleName)
        {
            Module module = netManager.GetModuleRequest(moduleName);
            return module;
        }

        public int SendExperimentUpdate(Experiment experiment)
        {
            int id = experiment.ExperimentId;
            string experiment_json = JsonConvert.SerializeObject(experiment);
            Debug.Log(experiment_json);
            int res = netManager.SendExperimentUpdateRequest(id, experiment_json);

            return res;
        }

    }
}