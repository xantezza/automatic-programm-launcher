using System;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Managers
{
    [Serializable]
    public class EventManager : IManager, ISaveable
    {
        [SerializeField] private Transform EventItemContentRoot;
        [SerializeField] private GameObject EventItemPrefab;

        [SerializeField] private SaveableData saveableData = new SaveableData();

        private SaveManager SaveManager => GameManager.Instance.SaveManager;

        public void Init()
        {
            SaveManager.Load(this);

            saveableData.eventsItemsData.ForEach(CreateEvent);

            saveableData.eventsItemsData.ForEach(x => Debug.Log(x.EventName));

            Debug.Log($"{nameof(EventManager)}: initialized");
        }

        public void CreateEvent(EventItemData data)
        {
            if (saveableData.eventsItemsData.All(x => x.EventId != data.EventId))
            {
                saveableData.eventsItemsData.Add(data);
                SaveManager.Save(this);
            }

            Object.Instantiate(EventItemPrefab, EventItemContentRoot).GetComponent<EventItem>().Init(data);
        }

        public void ModifyEvent(EventItemData unmodified, EventItemData modified)
        {
            var eventItemIndex = saveableData.eventsItemsData.FindIndex(x => x.EventId == unmodified.EventId);
            if (eventItemIndex == -1)
            {
                Debug.Log($"{nameof(EventManager)}: Unable to modify event with id {unmodified.EventId}. Not found.");
                return;
            }

            saveableData.eventsItemsData.RemoveAt(eventItemIndex);
            saveableData.eventsItemsData.Insert(eventItemIndex, modified);
            SaveManager.Save(this);
        }

        public void RemoveEvent(EventItemData toRemove)
        {
            var eventItemData = saveableData.eventsItemsData.FirstOrDefault(x => x.EventId == toRemove.EventId);
            if (eventItemData == null)
            {
                Debug.Log($"{nameof(EventManager)}: Unable to remove event with id {toRemove.EventId}. Not found.");
                return;
            }

            saveableData.eventsItemsData.Remove(eventItemData);
            SaveManager.Save(this);
        }

        #region ISaveable

        public string SaveKey => "EventManager";

        public string SaveData()
        {
            return JsonUtility.ToJson(saveableData);
        }

        public string DefaultData()
        {
            return SaveData();
        }

        public void LoadData(string serializedData)
        {
            saveableData = JsonUtility.FromJson<SaveableData>(serializedData);
        }
        #endregion

        [Serializable]
        public class SaveableData
        {
            public List<EventItemData> eventsItemsData = new List<EventItemData>();
        }
    }
}