using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Main
{
    public class CompletionPanel : MonoBehaviour
    {
        public GameObject panel;
        public static CompletionPanel instance;

        public event Action OnPressNoThanks;
        private void Awake()
        {
            instance = this;
        }
        public void OnNoThanks()
        {
            panel.SetActive(false);
            // Invoke the event safely
            OnPressNoThanks?.Invoke();
        }

        public void OpenCompletionScreen() {

            panel.SetActive(true);
        
        }
    }
}