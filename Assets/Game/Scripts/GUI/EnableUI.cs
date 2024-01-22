using Game.Scripts.LevelManager;
using UnityEngine;

namespace Game.Scripts.GUI
{
    public class EnableUI : MonoBehaviour
    {
        [SerializeField] private GameObject loseWindow;
        [SerializeField] private GameObject winWindow;


        private void Awake()
        {
            Level.WinEvent += WinEnable;
            Level.LoseEvent += LoseEnable;
        }

        private void LoseEnable()
        {
            loseWindow.SetActive(true);
        }
        private void WinEnable()
        {
            winWindow.SetActive(true);
        }

        private void OnDestroy()
        {
            Level.WinEvent -= WinEnable;
            Level.LoseEvent -= LoseEnable;
        }
    }
}