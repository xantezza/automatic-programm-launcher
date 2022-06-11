using Managers;
using TMPro;
using UI.PopupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EventItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI nameLabel;
        [SerializeField] private Button editButton;
        [SerializeField] private Button removeButton;

        private EventItemData cachedItemData;

        private PopupManager PopupManager => GameManager.Instance.PopupManager;
        private EventManager EventManager => GameManager.Instance.EventManager;

        private void Awake()
        {
            editButton.onClick.AddListener(OnEditClicked);
            removeButton.onClick.AddListener(OnRemoveClicked);
        }

        private void OnEditClicked()
        {
            if (PopupManager.TryGetPopup(out AdNewEventPopup popup))
            {
                popup.Show(OnEditingUserActionHandler, cachedItemData);
            }
        }

        private void OnRemoveClicked()
        {
            if (PopupManager.TryGetPopup(out ValidateUserActionPopup popup))
            {
                popup.Show(OnUserActionCallback);
            }
        }

        private void OnEditingUserActionHandler(EventItemData data)
        {
            EventManager.ModifyEvent(cachedItemData, data);
            Init(data);
        }

        public void Init(EventItemData data)
        {
            cachedItemData = data;
            nameLabel.text = data.EventName;
        }

        private void OnUserActionCallback(bool choice)
        {
            if (!choice) return;

            EventManager.RemoveEvent(cachedItemData);
            Destroy(gameObject);
        }
    }
}
