using System;
using System.Collections.Generic;
using System.Linq;
using UI.PopupSystem;
using UnityEngine;

namespace Managers
{
    [Serializable]
    public class PopupManager : IManager
    {
        private readonly List<Popup> popups = new List<Popup>();

        public  bool TryGetPopup<T>(out T popup) where T : Popup
        {
            popup = popups.FirstOrDefault(x => x is T) as T;
            return popup != null;
        }

        public void AddPopup(Popup popup)
        {
            popups.Add(popup);
        }

        public void Init()
        {

            Debug.Log($"{nameof(PopupManager)} initialized");
        }
    }
}