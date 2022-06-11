using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.PopupSystem
{
    public class ValidateUserActionPopup : Popup
    {
        [SerializeField] private Button YesButton;
        [SerializeField] private Button NoButton;

        private Action<bool> cachedCallback;

        protected override void Awake()
        {
            base.Awake();
            YesButton.onClick.AddListener(() => OnButtonClickHandler(true));
            NoButton.onClick.AddListener(() => OnButtonClickHandler(false));

            void OnButtonClickHandler(bool value)
            {
                InternalShow(false);
                cachedCallback?.Invoke(value);
            }
        }

        public void Show(Action<bool> onUserActionCallback)
        {
            cachedCallback = onUserActionCallback;
            InternalShow();
        } 
    }
}
