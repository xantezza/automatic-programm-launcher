using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PopupSystem
{
    public class AdNewEventPopup : Popup
    {
        [SerializeField] private TMP_InputField nameInputField;
        [SerializeField] private Button generateButton;
        [SerializeField] private Button closeButton;
        private Action<EventItemData> cachedCallback;

        protected override void Awake()
        {
            base.Awake();

            generateButton.onClick.AddListener(OnGenerateButtonClickedHandler);
            closeButton.onClick.AddListener(OnCloseButtonClickedHandler);
        }

        private void OnCloseButtonClickedHandler()
        {
            InternalShow(false);
        }

        private void OnGenerateButtonClickedHandler()
        {
            var eventItemData = new EventItemData(nameInputField.text);
            cachedCallback?.Invoke(eventItemData);
            InternalShow(false);
        }

        public void Show(Action<EventItemData> onUserActionCallback, EventItemData overrideDefaultEventItemData = null)
        {
            if (overrideDefaultEventItemData != null)
            {
                nameInputField.text = overrideDefaultEventItemData.EventName;
            }

            cachedCallback = onUserActionCallback;
            InternalShow();
        }
    }
}