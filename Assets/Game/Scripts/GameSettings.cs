using FMODUnity;
using Game.Scripts.SFX;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

namespace Game.Scripts
{
    public static class GameSettings
    {
        private const string MusicKey = "music_volume";
        private const string SoundKey = "sound_volume";

        private static float _musicVolume = 0;
        private static float _soundVolume = 0;

        public static float MusicVolume
        {
            get => _musicVolume;
            set
            {
                value = Mathf.Clamp(value, 0, 1);
                _musicVolume = value;

                if (_musicVolume <= 0)
                {
                    Debug.Log("Stopping music");
                    MusicController.StopMusic();
                }
                else
                {
                    Debug.Log("Playing music");
                    MusicController.PlayMusic("event:/MainMusic");
                }
            }
        }

        public static float SoundVolume
        {
            get => _soundVolume;
            set
            {
                value = Mathf.Clamp(value, 0, 1);
                _soundVolume = value;
            }
        }

        public static async void Load()
        {
            RuntimeManager.LoadBank("Master");
            while (!RuntimeManager.HaveAllBanksLoaded)
            {
                await Task.Yield();
            }

            while (RuntimeManager.AnySampleDataLoading())
            {
                await Task.Yield();
            }

            Debug.Log("banks loaded");
            MusicVolume = PlayerPrefs.GetFloat(MusicKey, 1);
            SoundVolume = PlayerPrefs.GetFloat(SoundKey, 1);
        }

        public static void Save()
        {
            PlayerPrefs.SetFloat(MusicKey, _musicVolume);
            PlayerPrefs.SetFloat(SoundKey, _soundVolume);
        }
    }
}