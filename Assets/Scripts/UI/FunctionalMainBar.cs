using Managers;
using UI.PopupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FunctionalMainBar : MonoBehaviour
    {
        [SerializeField] private Button autoStartButton;
        [SerializeField] private Button addNewEventButton;

        private PopupManager PopupManager => GameManager.Instance.PopupManager;
        private EventManager EventManager => GameManager.Instance.EventManager;

        private void Awake()
        {
            autoStartButton.onClick.AddListener(OnAutoStartButtonClickedHandler);
            addNewEventButton.onClick.AddListener(OnAddNewEventButtonClickedHandler);
        }

        private void OnAutoStartButtonClickedHandler()
        {
            //TODO:
            Debug.Log("Adding to autostart");
        }

        private void OnAddNewEventButtonClickedHandler()
        {
            if (PopupManager.TryGetPopup(out AdNewEventPopup popup))
            {
                popup.Show(AdNewEventPopupUserActionHandler);
            }

            void AdNewEventPopupUserActionHandler(EventItemData data)
            {
                EventManager.CreateEvent(data);
            }
        }
    }
}
