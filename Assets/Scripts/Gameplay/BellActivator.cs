using UnityEngine;
using UnityEngine.InputSystem;
using Game.Services;
using Game.Messages;

namespace Game
{
    public class BellActivator : MonoBehaviour
    {
        [SerializeField] private InputActionReference bellActionRef;

        private void Awake()
        {
            bellActionRef.action.performed += OnBellActivate;
        }

        private void OnBellActivate(InputAction.CallbackContext ctx)
        {
            MessagesService.Publish(new BellActivatedMessage());
        }

        private void OnDestroy()
        {
            bellActionRef.action.performed -= OnBellActivate;
        }
    }
}
