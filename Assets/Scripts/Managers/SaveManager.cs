using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Managers
{
    public interface ISaveable
    {
        string SaveKey { get; }

        string SaveData();
        string DefaultData();

        void LoadData(string serializedData);
    }

    [Serializable]
    public class SaveManager : IManager
    {
        public void Init()
        {
            Debug.Log($"{nameof(SaveManager)} initialized");
        }

        public void Save(ISaveable saveable)
        {
            PlayerPrefs.SetString(saveable.SaveKey, saveable.SaveData());

            Debug.Log($"{saveable.SaveKey} saved");
        }

        public void Load(ISaveable saveable)
        {
            saveable.LoadData(PlayerPrefs.GetString(saveable.SaveKey, saveable.DefaultData()));
            Debug.Log($"{saveable.SaveKey} loaded");
        }

        [Button]
        private void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
    }
}