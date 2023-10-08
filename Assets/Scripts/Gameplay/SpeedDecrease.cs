using UnityEngine;
using UnityEngine.InputSystem;
using Game.Services;
using Game.Messages;
using System;
using StarterAssets;

namespace Game
{
    public class SpeedDecrease : MonoBehaviour
    {
        [SerializeField] private ActivatableObject target;
        [SerializeField] private FirstPersonController fpsController;
        [SerializeField] private float minDistance = 2f;
        [SerializeField] private float maxDistance = 10f;

        private float initialSpeed;
        private float initialSprintSpeed;

        private void Awake()
        {
            initialSpeed = fpsController.MoveSpeed;
            initialSprintSpeed = fpsController.SprintSpeed;
        }

        private void Update()
        {
            if (target.IsActive)
            {
                var distance = (fpsController.transform.position - target.transform.position).magnitude;
                if (distance > maxDistance)
                {
                    fpsController.MoveSpeed = initialSpeed;
                    fpsController.SprintSpeed = initialSprintSpeed;
                }
                else if (distance <= minDistance)
                {
                    fpsController.MoveSpeed = 0;
                    fpsController.SprintSpeed = 0;
                }
                else
                {
                    var t = (distance - minDistance) / (maxDistance - minDistance);
                    fpsController.MoveSpeed = Mathf.Lerp(0, initialSpeed, t);
                    fpsController.SprintSpeed = Mathf.Lerp(0, initialSprintSpeed, t);
                }
            }
        }
    }
}
