using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.GUI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button soundTurnOffButton;
        [SerializeField] private Button soundTurnOnButton;
        [SerializeField] private Button musicTurnOffButton;
        [SerializeField] private Button musicTurnOnButton;
        [SerializeField] private Button rateUsButton;
        [SerializeField] private Button exitButton;


        private bool _soundButtonStatus;
        private bool _musicButtonStatus;

        private void Start()
        {
            soundTurnOffButton.gameObject.SetActive(GameSettings.SoundVolume == 1f);
            soundTurnOnButton.gameObject.SetActive(GameSettings.SoundVolume == 0f);
            musicTurnOffButton.gameObject.SetActive(GameSettings.MusicVolume == 1f);
            musicTurnOnButton.gameObject.SetActive(GameSettings.MusicVolume == 0f);

            soundTurnOffButton.onClick.AddListener(SoundTurnOff);
            soundTurnOnButton.onClick.AddListener(SoundTurnOn);
            musicTurnOffButton.onClick.AddListener(MusicTurnOff);
            musicTurnOnButton.onClick.AddListener(MusicTurnOn);
            // rateUsButton.onClick.AddListener(RateUs);
            exitButton.onClick.AddListener(OnExitButtonClick);
        }


        private void OnExitButtonClick()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        private void SoundTurnOff()
        {
            GameSettings.SoundVolume = 0f;
            soundTurnOffButton.gameObject.SetActive(false);
            soundTurnOnButton.gameObject.SetActive(true);
        }

        private void SoundTurnOn()
        {
            GameSettings.SoundVolume = 1f;
            soundTurnOnButton.gameObject.SetActive(false);
            soundTurnOffButton.gameObject.SetActive(true);
        }

        private void MusicTurnOff()
        {
            GameSettings.MusicVolume = 0f;
            musicTurnOffButton.gameObject.SetActive(false);
            musicTurnOnButton.gameObject.SetActive(true);
        }

        private void MusicTurnOn()
        {
            GameSettings.MusicVolume = 1f;
            musicTurnOnButton.gameObject.SetActive(false);
            musicTurnOffButton.gameObject.SetActive(true);
        }

        // private void RateUs()
        // {
        //     Application.OpenURL("https://play.google.com/store/games?hl=ru&gl=US");
        // }
    }
}