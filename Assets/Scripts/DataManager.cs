using Assets.Scripts.Utility;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
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
            netManager.SendRegistrationRequestAsync(registration_json);
            Debug.Log("In Register in DataManager");
            Debug.Log("Json : " + registration_json);
            return 0;
        }


        public int Login(AuthorisationRequest authorisationInfo)
        {

            string authprosation_json = JsonConvert.SerializeObject(authorisationInfo);
            Debug.Log("In Login in DataManager");
            Debug.Log("Json : " + authprosation_json);
            return 0;
        }

    }
}