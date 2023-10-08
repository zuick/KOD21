using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class ActivatableObject : MonoBehaviour
    {
        public UnityEvent OnActivate;
        public UnityEvent OnDeactivate;

        public bool IsActive { private set; get; }

        public void Activate()
        {
            IsActive = true;
            OnActivate.Invoke();
        }

        public void Deactivate()
        {
            IsActive = false;
            OnDeactivate.Invoke();
        }
    }
}
