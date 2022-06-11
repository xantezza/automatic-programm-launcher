using UI.PopupSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FunctionalMainBar : MonoBehaviour
    {
        [SerializeField] private Transform EventItemContentRoot;
        [SerializeField] private GameObject EventItemPrefab;
        [SerializeField] private Button autoStartButton;
        [SerializeField] private Button addNewEventButton;

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
            if (Popup.TryGetPopup(out AdNewEventPopup popup))
            {
                popup.Show(AdNewEventPopupUserActionHandler);
            }

            void AdNewEventPopupUserActionHandler(EventItemData data)
            {
               Instantiate(EventItemPrefab, EventItemContentRoot).GetComponent<EventItem>().Init(data);
            }
        }
    }
}
