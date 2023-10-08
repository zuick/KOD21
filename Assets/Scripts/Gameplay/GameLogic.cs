using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using Game.UI;
using Game.Services;
using Game.Messages;
using System;
using System.Collections.Generic;

namespace Game
{
    public class GameLogic : MonoBehaviour
    {
        [SerializeField] private WindowsService windowsService;
        [SerializeField] private FaderService faderService;
        [SerializeField] private InputActionReference inputActionReference;

        private ConfirmWindow exitGameWindow;
        private GameOverWindow gameOverWindow;
        private List<IDisposable> disposables = new();
        private bool finalReached;

        private void Awake()
        {
            windowsService.Open<PlayerHUDWindow>();
            inputActionReference.action.performed += OnCancel;
            disposables.Add(MessagesService.Subscribe<FogExtremeReached>(OnFogExtremeReached));
            disposables.Add(MessagesService.Subscribe<POIActivated>(OnPOITrigger));
        }

        private void OnPOITrigger(POIActivated e)
        {
            if (e.isFinal)
                finalReached = true;
        }

        private void OnFogExtremeReached(FogExtremeReached ctx)
        {
            var text = finalReached
                ? "Надежда словно призрачный фонарь,\nмелькает в темноте, обещая свет,\nно вместо этого лишь углубляет тени."
                : "Вы не справились с тьмой";

            gameOverWindow = windowsService.Open<GameOverWindow>(text);
            gameOverWindow.OnSubmit += OnRestart;
        }

        private void OnCancel(InputAction.CallbackContext obj)
        {
            if (!windowsService.IsActive(gameOverWindow))
            {
                if (!windowsService.IsActive(exitGameWindow))
                {
                    exitGameWindow = windowsService.Open<ConfirmWindow>("Выйти из игры?");
                    exitGameWindow.OnSubmit += OnExitGame;
                }
                else
                {
                    windowsService.Close(exitGameWindow);
                }
            }
        }

        private void OnRestart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }

        private void OnExitGame()
        {
            Application.Quit();
        }

        private void OnDestroy()
        {
            inputActionReference.action.performed -= OnCancel;
            foreach (var d in disposables)
            {
                d.Dispose();
            }
        }
    }
}
