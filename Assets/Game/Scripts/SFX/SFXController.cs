using System.Collections.Generic;
using System.Threading.Tasks;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Game.Scripts.SFX
{
    public static class SfxController
    {
        private static readonly Dictionary<string, GUID> Guids = new();

        public static void PlaySfx(string sfxKey, float maxPitchRange = 0)
        {
            if (GameSettings.SoundVolume <= 0)
                return;

            if (!Guids.ContainsKey(sfxKey))
                Guids.Add(sfxKey, RuntimeManager.PathToGUID(sfxKey));

            var eventInstance = RuntimeManager.CreateInstance(Guids[sfxKey]);
            if (maxPitchRange > 0)
            {
                eventInstance.getPitch(out float pitch);
                eventInstance.setPitch(pitch + Random.Range(-maxPitchRange, maxPitchRange));
            }
            eventInstance.start();
            ReleaseAfterPlay(eventInstance);
        }

        public static void PlayRandomSfx(float maxPitchRange = 0, params string[] sfxKeys) => PlaySfx(sfxKeys.GetRandomElement(), maxPitchRange);

        public static void StopSfx(EventInstance eventInstance)
        {
            eventInstance.stop(STOP_MODE.ALLOWFADEOUT);
            eventInstance.release();
        }

        private static async void ReleaseAfterPlay(EventInstance eventInstance)
        {
            eventInstance.getDescription(out var description);
            description.getLength(out int length);
            await Task.Delay(length);
            eventInstance.release();
        }
    }
}