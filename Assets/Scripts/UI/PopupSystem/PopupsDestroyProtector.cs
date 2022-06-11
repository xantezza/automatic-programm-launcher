using UnityEngine;

namespace UI.PopupSystem
{
    public class PopupsDestroyProtector : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(this);
        }
    }
}