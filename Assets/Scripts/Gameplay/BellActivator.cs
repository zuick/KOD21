using UnityEngine;
using UnityEngine.InputSystem;
using Game.Services;
using Game.Messages;

namespace Game
{
    public class BellActivator : MonoBehaviour
    {
        [SerializeField] private InputActionReference bellActionRef;
        [SerializeField] private AudioSource bellAudio;
        [SerializeField] private AudioSource notEnoughRingsAudio;
        [SerializeField] private int initialRings = 5;

        private void Awake()
        {
            bellActionRef.action.performed += OnBellActivate;
        }

        private void OnBellActivate(InputAction.CallbackContext ctx)
        {
            if (initialRings > 0)
            {
                bellAudio.Play();
                MessagesService.Publish(new BellActivatedMessage());
                initialRings--;
            }
            else
            {
                notEnoughRingsAudio.Play();
                MessagesService.Publish(new NotEnoughRings());
            }
        }

        private void OnDestroy()
        {
            bellActionRef.action.performed -= OnBellActivate;
        }
    }
}
