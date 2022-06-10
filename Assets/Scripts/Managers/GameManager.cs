using System;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action OnGameManagerAwake;

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(this);
            Instance = this;

            OnGameManagerAwake?.Invoke();
        }
    }
}
