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
            get => _soundVolume;
            set
            {
                value = Mathf.Clamp(value, 0, 1);
                _soundVolume = value;
            }
        }

        public static void Load()
        {
            MusicVolume = PlayerPrefs.GetFloat(MusicKey,1);
            SoundVolume = PlayerPrefs.GetFloat(SoundKey,1);
        }

        public static void Save()
        {
            PlayerPrefs.SetFloat(MusicKey, _musicVolume);
            PlayerPrefs.SetFloat(SoundKey, _soundVolume);
        }
    }
}