using Assets.Scripts.Level.Data;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Services
{
    public class LevelService
    {
        private LevelLibrary _levelLibrary;
        private LevelState _currentLevel = null;
        public LevelService(LevelLibrary sceneLibrary)
        {
            _levelLibrary = sceneLibrary;
        }

        public void LoadLevelById(string id)
        {
            var sceneData = _levelLibrary.GetLevelStateById(id);
            if (sceneData != null)
            {
                SceneManager.LoadScene(id);
                _currentLevel = sceneData;
            } else
            {
                SceneManager.LoadScene(0);
            }
        }
        public void SaveLevelById(LevelState stateData)
        {
            _levelLibrary.SaveLevelState(stateData);
        }
        public void LoadLevelFromManagerScene()
        {
           // _sceneLibrary.CleanLevelLibrary();
           _levelLibrary.LoadLevelsFromManagerScene();
            _levelLibrary.OpeningLvl();
        }

        public List<LevelState> GetAllLevels() => _levelLibrary.StateLevels;
        public void OpenNextLvl(string CurrentLevelId) 
        {
            _levelLibrary.OpenLevelById(CurrentLevelId);
        }
        public LevelState LoadNextLevel()
        {
            for (int i = 0; i < _levelLibrary.StateLevels.Count; i++)
            {
               var level = _levelLibrary.GetNextLevel(_currentLevel.Id);
                if (level != null)
                {
                    _currentLevel = level;
                    LoadLevelById(_currentLevel.Id);
                    break;
                }
            }
            return _currentLevel;
        }
        public void OpenLvlById(string id)
        {
            _levelLibrary.OpenLevelById(id);
        }

        public LevelState GetCurrentLevel() => _currentLevel;
        public void CurrentLevelIsFinish() => _levelLibrary.LevelIsFinish(_currentLevel);
        public void LevelIsFinish(LevelState currentLevel)
        {
            _levelLibrary.LevelIsFinish(currentLevel);
        }
    }
}
