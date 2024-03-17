using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Game.Scripts.SFX
{
    public static class MusicController
    {
        private static EventInstance _eventInstance;

        public static void PlayMusic(string musicKey)
        {
            return;
            if (!RuntimeManager.HasBankLoaded("Master"))
            {
                RuntimeManager.LoadBank("Master");
                Debug.Log("Master Bank try Loaded");
            }
            StopMusic();
            if (GameSettings.MusicVolume <= 0)
                return;
           
            _eventInstance = RuntimeManager.CreateInstance(musicKey);
            _eventInstance.start();
        }

        public static void StopMusic()
        {
            return;
            if (_eventInstance.Equals(default(EventInstance)))
                return;

            _eventInstance.stop(STOP_MODE.ALLOWFADEOUT);
            _eventInstance.release();
        }
    }
}