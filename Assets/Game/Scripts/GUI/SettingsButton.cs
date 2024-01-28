using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.GUI
{
    public class SettingsButton : MonoBehaviour
    {
        [SerializeField] private Button settingsButton;
        [SerializeField] private GameObject menuUI;
        

        private void Start()
        {
            settingsButton.onClick.AddListener(MenuEnable);
        }

        private void MenuEnable()
        {
            Time.timeScale = 0;
            menuUI.SetActive(true);
        }
    }
}