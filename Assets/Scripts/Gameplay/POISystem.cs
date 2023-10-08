using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Game.Services;
using Game.Messages;
using System;

namespace Game
{
    public class POISystem : MonoBehaviour
    {
        [SerializeField] private ActivatableObject[] points;

        private IDisposable POITriggerSubscription;
        private int currentPointIndex;

        private void Awake()
        {
            Activate(currentPointIndex);
            POITriggerSubscription = MessagesService.Subscribe<POIActivated>(OnPOITrigger);
        }

        private void Activate(int index)
        {
            for (int i = 0; i < points.Length; i++)
            {
                if (i == index)
                    points[i].Activate();
                else
                    points[i].Deactivate();
            }
        }

        private void OnPOITrigger(POIActivated e)
        {
            if (currentPointIndex < points.Length)
            {
                currentPointIndex++;
                Activate(currentPointIndex);
            }
            else
            {
                Activate(-1);
            }
        }

        private void OnDestroy()
        {
            POITriggerSubscription.Dispose();
        }
    }
}
