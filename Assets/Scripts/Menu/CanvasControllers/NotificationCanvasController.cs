using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Assets.Scripts.Menu
{

    public class NotificationCanvasController : CanvasController
    {

        [SerializeField]
        GameObject NotificationText;

        private string Notification_MESSAGE;

        public void SetNotificationMessage(string message)
        {
            Notification_MESSAGE = message;

            NotificationText.GetComponent<TextMeshProUGUI>().text = Notification_MESSAGE;
        }

        protected void Awake()
        {
            Debug.Log("In Awake in NotificationMessageMenu");
            this.menuCanvasType = MenuCanvasType.NotificationMessageMenu;
            Notification_MESSAGE = "Default notification message";

        }

    }



}