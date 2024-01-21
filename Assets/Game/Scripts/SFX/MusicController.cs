using FMOD.Studio;
using FMODUnity;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Game.Scripts.SFX
{
    public static class MusicController
    {
        private static EventInstance _eventInstance;

        public static void PlayMusic(string musicKey)
        {
            StopMusic();
            if (GameSettings.MusicVolume <= 0)
                return;

            _eventInstance = RuntimeManager.CreateInstance(musicKey);
            _eventInstance.start();
        }

        public static void StopMusic()
        {
            if (_eventInstance.Equals(default(EventInstance)))
                return;

            _eventInstance.stop(STOP_MODE.ALLOWFADEOUT);
            _eventInstance.release();
        }
    }
}