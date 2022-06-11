using UnityEngine;
﻿using System;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace UI.PopupSystem
{
    public class Popup : MonoBehaviour
    {
        public event Action<bool> Showing;
        public event Action<bool> Showed;

        [SerializeField] private GameObject gameObjectToSetActive;

        [SerializeField] private bool useAnimation;
        [ShowIf(nameof(useAnimation))] [SerializeField] private RectTransform rectTransformToAnimate;
        [ShowIf(nameof(useAnimation))] [SerializeField] private float minScale;
        [ShowIf(nameof(useAnimation))] [SerializeField] private float maxScale;
        [ShowIf(nameof(useAnimation))] [SerializeField] private float duration;

        public void Show(bool state, bool forceWithoutAnimation = false)
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
                    Showed?.Invoke(state);
                });

                return;
            }

            gameObjectToSetActive.SetActive(state);
            Showed?.Invoke(state);
        }
    }
}