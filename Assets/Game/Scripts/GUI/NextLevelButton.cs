using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.GUI
{
    public class NextLevelButton : MonoBehaviour
    {
        public static event Action OnClickEvent;
        [SerializeField] private Button reloadButton;

        private void Start()
        {
            reloadButton.onClick.AddListener(OnButtonClick);
        }

        private static void OnButtonClick()
        {
            OnClickEvent?.Invoke();
        }
    }
}