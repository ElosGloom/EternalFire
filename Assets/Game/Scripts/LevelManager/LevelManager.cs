using Game.Scripts.GUI;
using UnityEngine;

namespace Game.Scripts.LevelManager
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Level[] prefabs;

        private Level _currentLevel;
        private int _currentLevelIndex;


        private void Start()
        {
            _currentLevelIndex = PlayerPrefs.GetInt("Current Level Index");
            ReloadLevelButton.OnClickEvent += ReloadLevel;
            NextLevelButton.OnClickEvent += LoadNextLevel;
            _currentLevel = Instantiate(prefabs[_currentLevelIndex]);
        }

        private void ReloadLevel()
        {
            Destroy(_currentLevel.gameObject);
            _currentLevel = Instantiate(prefabs[_currentLevelIndex]);
        }

        private void LoadNextLevel()
        {
            Destroy(_currentLevel.gameObject);
            _currentLevelIndex++;

            if (_currentLevelIndex >= prefabs.Length) 
                _currentLevelIndex = 0;

            PlayerPrefs.SetInt("Current Level Index", _currentLevelIndex);
            PlayerPrefs.Save();
            _currentLevel = Instantiate(prefabs[_currentLevelIndex]);
        }

        private void OnDestroy()
        {
            ReloadLevelButton.OnClickEvent -= ReloadLevel;
            NextLevelButton.OnClickEvent += LoadNextLevel;
        }
    }
}