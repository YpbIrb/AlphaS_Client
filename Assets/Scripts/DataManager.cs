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


        public int Register(RegistrationRequest registrationInfo)
        {
            string registration_json = JsonConvert.SerializeObject(registrationInfo);
            Participant part = netManager.SendRegistrationRequestAsync(registration_json);
            if(part == null)
            {
                return 0;
            }

            int part_id;
            string partId_string = part.ParticipantId;
            partId_string = new string(partId_string.Where(c => char.IsDigit(c)).ToArray());

            bool parse_success = int.TryParse(partId_string, out part_id);

            return part_id;
        }

        public int Login(AuthorisationRequest authorisationInfo)
        {
            string authprosation_json = JsonConvert.SerializeObject(authorisationInfo);
            Debug.Log("In Login in DataManager");
            Debug.Log("Json : " + authprosation_json);
            return 0;
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

    }
}