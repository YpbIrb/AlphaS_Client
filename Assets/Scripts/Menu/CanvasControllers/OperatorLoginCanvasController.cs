using Assets.Scripts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Menu
{
    public class OperatorLoginCanvasController : CanvasController
    {
        [SerializeField]
        TMP_InputField LoginInputField;

        [SerializeField]
        TMP_InputField PassInputField;



        protected void Awake()
        {
            Debug.Log("In Awake in ExperimentIdCanvasController");
            this.menuCanvasType = MenuCanvasType.OperatorLoginMenu;

        }


        public OperatorLoginRequest GetOperatorLoginRequest()
        {
            OperatorLoginRequest res = new OperatorLoginRequest();
            res.Password = GetOperatorPassword();
            res.UserName = GetOperatorLogin();
            return res;
        }

        public string GetOperatorLogin()
        {

            Debug.Log("login length before filter : " + LoginInputField.text);


            string login = new string(LoginInputField.text.Where(c => char.IsLetterOrDigit(c)).ToArray());
            Debug.Log("login length after filter : " + login);
            return login;
        }

        public string GetOperatorPassword()
        {
            string pass = new string(PassInputField.text.Where(c => char.IsLetterOrDigit(c)).ToArray());
            return pass;
        }

    }
}
