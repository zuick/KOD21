using UnityEngine;
using UnityEngine.InputSystem;
using Game.Services;
using Game.Messages;
using System;

namespace Game
{
    public class BellActivator : MonoBehaviour
    {
        [SerializeField] private InputActionReference bellActionRef;
        [SerializeField] private AudioSource bellAudio;
        [SerializeField] private AudioSource notEnoughRingsAudio;
        [SerializeField] private int initialRings = 3;

        private int rings;

        private IDisposable POITriggerSubscription;

        private void Awake()
        {
            rings = initialRings;
            bellActionRef.action.performed += OnBellActivate;
            POITriggerSubscription = MessagesService.Subscribe<POITrigger>(OnPOITrigger);
        }

        private void OnPOITrigger(POITrigger e)
        {
            rings = initialRings;
        }

        private void OnBellActivate(InputAction.CallbackContext ctx)
        {
            if (rings > 0)
            {
                bellAudio.Play();
                MessagesService.Publish(new BellActivatedMessage());
                rings--;
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
            POITriggerSubscription.Dispose();
        }

        private void OnGUI()
        {
            GUILayout.Space(20);
            GUILayout.BeginHorizontal();
            GUILayout.Space(20);
            GUILayout.Label($"Rings: {rings}");
            GUILayout.EndHorizontal();
        }
    }
}
