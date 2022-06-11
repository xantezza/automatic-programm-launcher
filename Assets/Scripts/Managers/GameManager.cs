using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action GameManagerInitialized;

        public EventManager EventManager;
        public SaveManager SaveManager;
        public PopupManager PopupManager;

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Debug.Log($"{nameof(GameManager)} initializing");

            DontDestroyOnLoad(this);
            Instance = this;

            InitManagers();

            Debug.Log($"{nameof(GameManager)} initialized");

            GameManagerInitialized?.Invoke();
        }

        private void InitManagers()
        {
            EventManager.Init();
            SaveManager.Init();
            PopupManager.Init();
    }
    }
}
