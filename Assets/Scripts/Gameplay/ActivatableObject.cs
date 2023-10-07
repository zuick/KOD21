using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Game.Services;
using Game.Messages;
using System;

namespace Game
{
    public class ActivatableObject : MonoBehaviour
    {
        public UnityEvent OnActivate;
        public UnityEvent OnDeactivate;

        public void Activate() { OnActivate.Invoke(); }
        public void Deactivate() { OnDeactivate.Invoke(); }

    }
}
