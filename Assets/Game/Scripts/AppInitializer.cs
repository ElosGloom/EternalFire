using Game.Scripts.SFX;
using UnityEngine;

namespace Game.Scripts
{
    public class AppInitializer : MonoBehaviour
    {
        private void Awake()
        {
            MusicController.PlayMusic("event:/MainMusic");
        }
    }
}