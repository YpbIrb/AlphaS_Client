using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;




namespace Assets.Scripts.Menu
{
    public enum MenuCanvasType
    {
        IdentificationTypeChoiceMenu,
        RegistrationMenu,
        AuthorisationMenu,
        MainMenu,
        ErrorMessageMenu
    }



    public class MenuCanvasManager: Singleton<MenuCanvasManager>
    {

        List<CanvasController> canvasControllerList;
        CanvasController lastActiveCanvas;

        Canvas canvas;



        protected override void Awake()
        {
            base.Awake();
            canvasControllerList = GetComponentsInChildren<CanvasController>().ToList();
            canvas = GetComponentInChildren<Canvas>();
            canvasControllerList.ForEach(x => {
                Debug.Log("Deactivateing Canvas with type: " + x.menuCanvasType);
                x.gameObject.SetActive(false);
            });
            OpenCanvas(MenuCanvasType.IdentificationTypeChoiceMenu);
        }

        public void OpenCanvas(MenuCanvasType _type)
        {
            if (lastActiveCanvas != null)
            {
                lastActiveCanvas.gameObject.SetActive(false);
            }

            CanvasController desiredCanvas = canvasControllerList.Find(x => x.menuCanvasType == _type);
            if (desiredCanvas != null)
            {
                desiredCanvas.gameObject.SetActive(true);
                lastActiveCanvas = desiredCanvas;
            }
            else { Debug.LogWarning("The desired canvas was not found!"); }

            if (!canvas.enabled)
            {
                canvas.enabled = true;
            }

        }

        public CanvasController GetCanvasControllerByType(MenuCanvasType _type)
        {
            CanvasController desiredCanvas = canvasControllerList.Find(x => x.menuCanvasType == _type);
            if (desiredCanvas != null)
            {
                return desiredCanvas;
            }
            else
            {

                Debug.LogWarning("The desired canvas was not found!");
                return null;
            }
        }

        public RegistrationCanvasController GetRegistrationCanvasController()
        {
            return (RegistrationCanvasController)GetCanvasControllerByType(MenuCanvasType.RegistrationMenu);
        }

        public AuthorisationCanvasController GetAuthorisationCanvasController()
        {
            return (AuthorisationCanvasController)GetCanvasControllerByType(MenuCanvasType.AuthorisationMenu);
        }

        public ErrorCanvasController GetErrorCanvasController()
        {
            return (ErrorCanvasController)GetCanvasControllerByType(MenuCanvasType.ErrorMessageMenu);
        }

        public void DisableMenu()
        {
            canvas.enabled = false;
        }

        public void EnableMenu()
        {
            canvas.enabled = true;
        }

        public void ShowError()
        {
            OpenCanvas(MenuCanvasType.ErrorMessageMenu);
        }

        public void CloseError()
        {

        }


    }
}