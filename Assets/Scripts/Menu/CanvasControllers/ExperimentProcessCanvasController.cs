using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Menu
{

    public class ExperimentProcessCanvasController : CanvasController
    {
        [SerializeField]
        GameObject CurrentModuleNameTextField;

        [SerializeField]
        GameObject CurrentModuleOrderTextField;

        [SerializeField]
        GameObject CurrentModuleConditionTextField;



        private const string name_base = "Current Module : ";
        private const string condition_base = "Condition : ";
        private const string order_base = "Order : ";

        protected void Awake()
        {
            Debug.Log("In Awake in ExperimentProcessCanvasController");
            this.menuCanvasType = MenuCanvasType.ExperimentProcessMenu;
        }

        public void SetCurrentModuleName(string moduleName)
        {
            CurrentModuleNameTextField.GetComponent<TextMeshProUGUI>().text = name_base + moduleName;
        }

        public void SetCurrentModuleCondition(string condition)
        {
            CurrentModuleConditionTextField.GetComponent<TextMeshProUGUI>().text = condition_base + condition;
        }

        public void SetCurrentModuleOrder(int order)
        {
            CurrentModuleConditionTextField.GetComponent<TextMeshProUGUI>().text = order_base + order;
        }

    }

}

