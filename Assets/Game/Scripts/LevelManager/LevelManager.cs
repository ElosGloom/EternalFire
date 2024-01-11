using System;
using Game.Scripts.Health;
using UnityEngine;

namespace Game.Scripts.LevelManager
{
    public class LevelManager : MonoBehaviour
    {
        private Level _currentLevel;
        [SerializeField] private Level[] prefabs;


        private void Start()
        {
            Level.LoseEvent += ReloadLevel;
            _currentLevel = Instantiate(prefabs[0]);
            // _currentLevel.WinEvent += LoadNextLevel;
        }

        private void ReloadLevel()
        {
            Destroy(_currentLevel.gameObject);
            Debug.Log("YOU LOSE");
            _currentLevel = Instantiate(prefabs[0]);
        }

        private void OnDestroy()
        {
            // _currentLevel.WinEvent -= LoadNextLevel;
            Level.LoseEvent -= ReloadLevel;
        }
    }
}