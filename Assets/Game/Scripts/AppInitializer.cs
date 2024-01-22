using Game.Scripts.SFX;
using UnityEngine;

namespace Game.Scripts
{
    public class AppInitializer : MonoBehaviour
    {
        private void Awake()
        {
            Application.targetFrameRate = 60;
            MusicController.PlayMusic("event:/MainMusic");
        }
    }
}