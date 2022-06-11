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

        private void Awake()
        {
            editButton.onClick.AddListener(OnEditClicked);
            removeButton.onClick.AddListener(OnRemoveClicked);

            void OnEditClicked()
            {
                if (Popup.TryGetPopup(out AdNewEventPopup popup))
                {
                    popup.Show(OnEditingUserActionHandler, cachedItemData);
                }
            }

            void OnEditingUserActionHandler(EventItemData data)
            {
                Init(data);
            }

            void OnRemoveClicked()
            {
                if (Popup.TryGetPopup(out ValidateUserActionPopup popup))
                {
                    popup.Show(OnUserActionCallback);
                }
            }
        }

        public void Init(EventItemData data)
        {
            cachedItemData = data;
            nameLabel.text = data.EventName;
        }

        private void OnUserActionCallback(bool choice)
        {
            if (choice)
            {
                Destroy(gameObject);
            }
        }
    }
}
