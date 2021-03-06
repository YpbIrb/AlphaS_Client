using Assets.Scripts.Menu;
using Assets.Scripts.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts
{

    public class ExperimentManager
    {
        public const string base_module_path = "C:\\AlphaS\\Modules\\";
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

        NamedPipeResultsGetter namedPipeResultsGetter;

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

            namedPipeResultsGetter = new NamedPipeResultsGetter();

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
            namedPipeResultsGetter = new NamedPipeResultsGetter();
            curr_module_order = 1;
        }

        public void StartExperiment()
        {
            experiment.StartTime = DateTime.Now;
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

            var resultsTask = namedPipeResultsGetter.GetModuleResults(moduleInExperiment.ModuleName);

            experimentProcessCanvasController.SetCurrentModuleName(moduleInExperiment.ModuleName);
            experimentProcessCanvasController.SetCurrentModuleCondition("Starting");
            experimentProcessCanvasController.SetCurrentModuleOrder(curr_module_order);

            System.Diagnostics.Process myProcess = new System.Diagnostics.Process();
            Module module = GetModuleByName(moduleInExperiment.ModuleName);
            myProcess.StartInfo.FileName = base_module_path +module.PathToExe + ".exe.lnk";
            myProcess.StartInfo.CreateNoWindow = false;
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
            foreach (KeyValuePair<string, string> pair in moduleInExperiment.InputValues)
            {
                myProcess.StartInfo.Arguments += pair.Key + "=" + pair.Value+" ";
            }
            myProcess.StartInfo.Arguments += "first_part" + "=" + experiment.FirstParticipant.ParticipantId + " ";
            myProcess.StartInfo.Arguments += "second_part" + "=" + experiment.SecondParticipant.ParticipantId + " ";

            try
            {
                moduleInExperiment.StartTime = DateTime.Now;
                myProcess.Start();
                experimentProcessCanvasController.SetCurrentModuleCondition("Started");
                myProcess.WaitForExit();
                moduleInExperiment.FinishTime = DateTime.Now;
                Dictionary<string, string> module_res = new Dictionary<string, string>(namedPipeResultsGetter.results);
                moduleInExperiment.OutputValues = module_res;
                experimentProcessCanvasController.SetCurrentModuleCondition("Finished");
            }
            catch (Exception e)
            {
                experimentProcessCanvasController.SetCurrentModuleCondition("Exception while opening " + myProcess.StartInfo.FileName);
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

                    AllModules.Add(res);
                    //todo чек на то, что модуль нашелся нормально(по факту, может не найтись, только если сервер накрылся)
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
            experiment.FinishTime = DateTime.Now;

            if (experiment.FirstParticipant.ParticipantId == 0)
                experiment.FirstParticipant = firstParticipant;

            if (experiment.SecondParticipant.ParticipantId == 0)
                experiment.SecondParticipant = secondParticipant;

            return experiment;
        }

        public List<string> GetMissingExecs()
        {
            List<string> res = new List<string>();
            foreach(ModuleInExperiment moduleInExperiment in experiment.Modules)
            {
                string path;
                Module module = GetModuleByName(moduleInExperiment.ModuleName);
                path = base_module_path + module.PathToExe + ".exe.lnk";
                if (!File.Exists(path))
                {
                    res.Add(module.PathToExe);
                }
            }
            return res;
        }

    }


    public class NamedPipeResultsGetter
    {
        public const string pipe_name = "AlphaS";
        public NamedPipeServerStream namedPipeServerStream;
        public Dictionary<string, string> results;


        public NamedPipeResultsGetter()
        {
            results = new Dictionary<string, string>();
        }

        public async Task<Dictionary<string, string>> GetModuleResults(string moduleName)
        {
            results.Clear();
            await Task.Factory.StartNew(() =>
            {
                namedPipeServerStream = new NamedPipeServerStream(pipe_name, PipeDirection.In);
                using (StreamReader sr = new StreamReader(namedPipeServerStream))
                {
                    string temp;
                    Debug.Log("Waiting for results of module " + moduleName + "in GetModuleResults");
                    namedPipeServerStream.WaitForConnection();
                    Debug.Log("ClientConnected");
                    // Wait for 'sync message' from the server.
                    do
                    {
                        temp = sr.ReadLine();
                    }
                    while (!temp.StartsWith(moduleName));

                    while ((temp = sr.ReadLine()) != "End")
                    {
                        KeyValuePair<string, string> newValuePair = ParseLine(temp);
                        if (newValuePair.Key != "Error while splitting")
                            results.Add(newValuePair.Key, newValuePair.Value);
                        Debug.Log("Get from " + moduleName + " : " + temp);
                    }
                    //namedPipeServerStream.
                }
            });
            
            return results;

        }

        private KeyValuePair<string, string> ParseLine(string line)
        {
            string[] splitted = line.Split('=');
            if (splitted.Length == 2)
            {
                return new KeyValuePair<string, string>(splitted[0], splitted[1]);
            }
            else
            {
                return new KeyValuePair<string, string>("Error while splitting", "");
            }
        }

    }


}

