using Assets.Scripts.Menu;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts
{

    public class ExperimentManager
    {
        public List<Module> AllModules { get; set; }
        public Experiment experiment { get; set; }
        ApplicationController applicationController;
        ApplicationView applicationView;
        MenuCanvasManager canvasManager;

        ParticipantInExperiment firstParticipant;
        ParticipantInExperiment secondParticipant;

        ExperimentProcessCanvasController experimentProcessCanvasController;

        DataManager dataManager;
        int curr_module_order;
        int last_module_order;

        //Пайпа тоже должна быть где-то тут


        public ExperimentManager(Experiment experiment)
        {
            applicationController = ApplicationController.GetInstance();
            applicationView = ApplicationView.GetInstance();
            dataManager = DataManager.GetInstance();
            canvasManager = MenuCanvasManager.GetInstance();
            experimentProcessCanvasController = canvasManager.GetExperimentProcessCanvasController();
            this.experiment = experiment;
            curr_module_order = 1;

            firstParticipant = new ParticipantInExperiment();
            secondParticipant = new ParticipantInExperiment();
            //Открываем пайп с названием AlphaS, который должен получать Dictionary <string string>, засериалайзеный в json 
        }

        public ExperimentManager()
        {
            applicationController = ApplicationController.GetInstance();
            applicationView = ApplicationView.GetInstance();
            dataManager = DataManager.GetInstance();
            canvasManager = MenuCanvasManager.GetInstance();
            experimentProcessCanvasController = canvasManager.GetExperimentProcessCanvasController();
            firstParticipant = new ParticipantInExperiment();
            secondParticipant = new ParticipantInExperiment();
            curr_module_order = 1;
        }

        public void StartExperiment()
        {
            foreach(ModuleInExperiment module in experiment.Modules)
            {
                if (module.ModuleOrder > last_module_order)
                    last_module_order = module.ModuleOrder;
            }
            
            curr_module_order = 1;
            ModuleInExperiment curr_module = GetModuleInExperimentByOrder(curr_module_order);
            Debug.Log(curr_module);
            ExecuteModule(curr_module);
            //Итерируемся по всем модулям, и ждем закрытия предидущего, прежде чем запускать следующий


            /*
            while(curr_module != null)
            {
                Debug.Log("Executeing module with order = " + curr_module.ModuleOrder);

                ExecuteModule(curr_module);
                curr_module_order++;
                curr_module = GetModuleInExperimentByOrder(curr_module_order);
            }
            */
            //Debug.Log("All Modules executed");

        }

        public void ContinueExperiment()
        {
            curr_module_order++;
            if(curr_module_order >= last_module_order)
            {
                experimentProcessCanvasController.FinalModuleOn();
            } 

            ModuleInExperiment curr_module = GetModuleInExperimentByOrder(curr_module_order);
            if(curr_module == null)
            {
                experimentProcessCanvasController.SetCurrentModuleName("No Module");
                experimentProcessCanvasController.SetCurrentModuleCondition("Can't find module with order " + curr_module_order);
            }
            else{
                ExecuteModule(curr_module);
            }
            
        }

        private void ExecuteModule(ModuleInExperiment moduleInExperiment)
        {
            //Сначала запустить экзе с нужными параметрами, а потом добавить слушателя на пипу
            //После получения инфу из пипы, запихиваем её в соответствующий ModuleInExperiment

            experimentProcessCanvasController.SetCurrentModuleName(moduleInExperiment.ModuleName);
            experimentProcessCanvasController.SetCurrentModuleCondition("Starting");
            experimentProcessCanvasController.SetCurrentModuleOrder(curr_module_order);

            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            Module module = GetModuleByName(moduleInExperiment.ModuleName);
            myProcess.StartInfo.FileName = "F:\\Modules\\" +module.PathToExe + ".exe.lnk";
            myProcess.StartInfo.CreateNoWindow = false;
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            foreach (KeyValuePair<string, string> pair in moduleInExperiment.InputValues)
            {
                myProcess.StartInfo.Arguments += pair.Key + "=" + pair.Value+" ";
            }

            try
            {
                myProcess.Start();
                experimentProcessCanvasController.SetCurrentModuleCondition("Started");
                myProcess.WaitForExit();
                experimentProcessCanvasController.SetCurrentModuleCondition("Finished");
            }
            catch (Exception e)
            {
                experimentProcessCanvasController.SetCurrentModuleCondition("Esception while opening " + myProcess.StartInfo.FileName);
                Debug.Log(e);
            }
        }

        private ModuleInExperiment GetModuleInExperimentByOrder(int order)
        {
            ModuleInExperiment module = experiment.Modules.Find(e => e.ModuleOrder == order);
            return module;
        }

        private Module GetModuleByName(string moduleName)
        {
            Module res;
            Debug.Log("Getting module with name : " + moduleName);

            if (AllModules == null)
            {
                Debug.Log("AllModules not initialized. Getting module " + moduleName + " from dataManager");
                res = dataManager.GetModuleByName(moduleName);
            }
            else 
            {
                res = AllModules.Find(module => module.ModuleName == moduleName);
                if (res == null)
                {
                    Debug.Log("No module with name " + moduleName + " in AllModule list. Getting module " + moduleName + " from dataManager");
                    res = dataManager.GetModuleByName(moduleName);
                }
                else
                {
                    Debug.Log("Module with name " + moduleName + " is founded in AllModules. Using it.");
                }
            }
            return res;
        }

        public void SetParticipantId(int part_num, int id)
        {
            switch (part_num)
            {
                case 1:
                    firstParticipant.ParticipantId = id;
                    break;

                case 2:
                    secondParticipant.ParticipantId = id;
                    break;

                default:
                    break;
            }
        }

        public void SetParticipantInExperimentInfo(int part_num, ParticipantInExperiment participantInExperiment)
        {
            switch (part_num)
            {
                case 1:
                    firstParticipant.HeadInjury = participantInExperiment.HeadInjury;
                    firstParticipant.Intoxication = participantInExperiment.Intoxication;
                    firstParticipant.Periods = participantInExperiment.Periods;
                    firstParticipant.AdditionalInfo = participantInExperiment.AdditionalInfo;
                    Debug.Log("First participant info saved : ");
                    Debug.Log(firstParticipant);
                    break;

                case 2:
                    secondParticipant.HeadInjury = participantInExperiment.HeadInjury;
                    secondParticipant.Intoxication = participantInExperiment.Intoxication;
                    secondParticipant.Periods = participantInExperiment.Periods;
                    secondParticipant.AdditionalInfo = participantInExperiment.AdditionalInfo;
                    Debug.Log("Second participant info saved : ");
                    Debug.Log(secondParticipant);
                    break;

                default:
                    break;
            }
        }

        public Experiment GetFinalExperimentInfo() 
        {
            if(experiment.FirstParticipant == null)
                experiment.FirstParticipant = firstParticipant;

            if (experiment.SecondParticipant == null)
                experiment.SecondParticipant = secondParticipant;

            return experiment;
        }

    }

}

