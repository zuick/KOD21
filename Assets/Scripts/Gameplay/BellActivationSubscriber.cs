using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Game.Services;
using Game.Messages;
using System;

namespace Game
{
    public class BellActivationSubscriber : MonoBehaviour
    {
        public UnityEvent OnBellActivated;

        private IDisposable subscription;

        private void Awake()
        {
            subscription = MessagesService.Subscribe<BellActivatedMessage>(ProcessBellActivated);
        }

        private void ProcessBellActivated(BellActivatedMessage e)
        {
            if(gameObject.activeInHierarchy)
                OnBellActivated.Invoke();
        }

        private void OnDestroy()
        {
            subscription.Dispose();
        }
    }
}
