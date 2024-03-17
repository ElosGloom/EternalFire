using FPS.LocalizationService;
using UnityEngine;

namespace Game.Scripts
{
    public class AppInitializer : MonoBehaviour
    {
        private void Awake()
        {
            
            GameSettings.Load();
            Application.targetFrameRate = 60;
            Localization.Init();
        }

        private void OnApplicationQuit()
        {
            GameSettings.Save();
            PlayerPrefs.Save();
        }
        
    }
}