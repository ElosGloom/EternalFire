using Game.Scripts.SFX;
using UnityEngine;

namespace Game.Scripts
{
    public static class GameSettings
    {
        private const string MusicKey = "music_volume";
        private const string SoundKey = "sound_volume";

        private static float _musicVolume = 1;
        private static float _soundVolume = 1;

        public static float MusicVolume
        {
            get => _musicVolume;
            set
            {
                value = Mathf.Clamp(value, 0, 1);
                _musicVolume = value;

                if (_musicVolume <= 0)
                    MusicController.StopMusic();
                else
                    MusicController.PlayMusic("event:/MainMusic");
            }
        }

        public static float SoundVolume
        {
            get => _musicVolume;
            set
            {
                value = Mathf.Clamp(value, 0, 1);
                _musicVolume = value;
            }
        }

        public static void Load()
        {
            _musicVolume = PlayerPrefs.GetFloat(MusicKey);
            _soundVolume = PlayerPrefs.GetFloat(SoundKey);
        }

        public static void Save()
        {
            PlayerPrefs.SetFloat(MusicKey, _musicVolume);
            PlayerPrefs.SetFloat(SoundKey, _soundVolume);
        }
    }
}