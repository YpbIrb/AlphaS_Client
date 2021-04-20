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

        [SerializeField]
        GameObject ContinueExperimentButton;

        [SerializeField]
        GameObject FinishExperimentButton;


        private const string name_base = "Текущий модуль : ";
        private const string condition_base = "Состояние модуля : ";
        private const string order_base = "Порядок модуля : ";

        protected void Awake()
        {
            Debug.Log("In Awake in ExperimentProcessCanvasController");
            this.menuCanvasType = MenuCanvasType.ExperimentProcessMenu;
            FinishExperimentButton.SetActive(false);
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
            CurrentModuleOrderTextField.GetComponent<TextMeshProUGUI>().text = order_base + order;
        }

        public void FinalModuleOn()
        {
            ContinueExperimentButton.SetActive(false);
            FinishExperimentButton.SetActive(true);
        }

    }

}

