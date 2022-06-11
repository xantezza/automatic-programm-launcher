using UnityEngine;
﻿using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace UI.PopupSystem
{
    public class Popup : MonoBehaviour
    {
        public event Action<bool> Showing;
        public event Action<bool> Showed;

        private static readonly List<Popup> popups = new List<Popup>();

        [SerializeField] private GameObject gameObjectToSetActive;
        [SerializeField] private bool useAnimation;
        [ShowIf(nameof(useAnimation))] [SerializeField] private RectTransform rectTransformToAnimate;
        [ShowIf(nameof(useAnimation))] [SerializeField] private float minScale;
        [ShowIf(nameof(useAnimation))] [SerializeField] private float maxScale;
        [ShowIf(nameof(useAnimation))] [SerializeField] private float duration;

        protected virtual void Awake()
        {
            popups.Add(this);
        }

        protected void InternalShow(bool state = true, bool forceWithoutAnimation = false)
        {
            Showing?.Invoke(state);

            if (!forceWithoutAnimation && useAnimation)
            {
                gameObjectToSetActive.SetActive(true);

                var startvalue = state ? minScale : maxScale;
                var endvalue = state ? maxScale : minScale;

                rectTransformToAnimate.localScale = startvalue * Vector3.one;

                rectTransformToAnimate.DOScale(endvalue, duration).OnComplete(() =>
                {
                    gameObjectToSetActive.SetActive(state);
                    rectTransformToAnimate.localScale = maxScale * Vector3.one;
                    Showed?.Invoke(state);
                });

                return;
            }

            gameObjectToSetActive.SetActive(state);
            Showed?.Invoke(state);
        }

        public static bool TryGetPopup<T>(out T popup) where T : Popup
        {
            popup = popups.FirstOrDefault(x => x is T) as T;
            return popup != null;
        }
    }
}