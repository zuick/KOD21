using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Game.Services;
using Game.Messages;
using System;

namespace Game
{
    public class POITrigger : MonoBehaviour
    {
        public UnityEvent OnTrigger;
        [SerializeField] private bool isFinal;

        private void OnTriggerEnter(Collider other)
        {
            MessagesService.Publish(new POIActivated(isFinal));
            OnTrigger.Invoke();
        }
    }
}
