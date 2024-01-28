using System;
using Game.Scripts.SFX;
using UnityEngine;

namespace Game.Scripts
{
    public class AppInitializer : MonoBehaviour
    {
        private void Awake()
        {
            GameSettings.Load();
            Application.targetFrameRate = 60;
        }

        private void OnApplicationQuit()
        {
            GameSettings.Save();
            PlayerPrefs.Save();
        }
        
    }
}