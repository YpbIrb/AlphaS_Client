using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Assets.Scripts.Menu
{

    public class NotificationCanvasController : CanvasController
    {

        TMP_Text Notification_Text;

        private string Notification_MESSAGE;

        public void SetNotificationMessage(string message)
        {
            Notification_MESSAGE = message;

            if (Notification_Text == null)
            {
                Debug.Log("Notification_Text = NULL");
            }


            Notification_Text.text = Notification_MESSAGE;
        }

        protected void Awake()
        {
            Debug.Log("In Awake in NotificationMessageMenu");
            this.menuCanvasType = MenuCanvasType.NotificationMessageMenu;
            Notification_MESSAGE = "Default notification message";
            Notification_Text = GetComponentInChildren<TMPro.TMP_Text>();
            Notification_Text.text = Notification_MESSAGE;

        }

    }



}