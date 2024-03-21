using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Scripts.SFX
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        public Sound[] sounds;

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.volume = s.volume;
                s.source.pitch = s.pitch;
                s.source.loop = s.loop;
            }
        }

        public void PlaySfx(string sound)
        {
            if (GameSettings.SoundVolume <= 0)
                return;

            Sound s = Array.Find(sounds, item => item.name == sound);
            s.source.pitch = Random.Range(0.8f, 1.2f);
            s.source.Play();
        }

        public void PlayMusic(string sound)
        {
            StopMusic(sound);
            if (GameSettings.MusicVolume <= 0)
                return;

            Sound s = Array.Find(sounds, item => item.name == sound);
            s.source.Play();
        }

        public void PlayRandomSfx(params string[] sfxKeys)
        {
            PlaySfx(sfxKeys.GetRandomElement());
        }

        public void StopSfx(string sound)
        {
            Sound s = Array.Find(sounds, item => item.name == sound);
            s.source.Stop();
        }

        public void StopMusic(string sound)
        {
            Sound s = Array.Find(sounds, item => item.name == sound);
            s.source.Stop();
        }
    }
}