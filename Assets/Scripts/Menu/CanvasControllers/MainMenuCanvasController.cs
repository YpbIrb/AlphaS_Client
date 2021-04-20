using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Menu
{
    public class MainMenuCanvasController : CanvasController
    {


        protected void Awake()
        {
            Debug.Log("In Awake in MainCanvasController");
            this.menuCanvasType = MenuCanvasType.MainMenu;
        }
    }
}