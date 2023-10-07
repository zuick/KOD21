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

        private void OnTriggerEnter(Collider other)
        {
            MessagesService.Publish(new POITrigger());
            OnTrigger.Invoke();
        }
    }
}
