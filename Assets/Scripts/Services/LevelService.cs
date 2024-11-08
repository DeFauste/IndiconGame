using Assets.Scripts.Level.Data;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Services
{
    public class LevelService
    {
        private LevelLibrary _sceneLibrary;
        public LevelService(LevelLibrary sceneLibrary)
        {
            _sceneLibrary = sceneLibrary;
        }

        public void LoadSceneById(string id)
        {
            var sceneData = _sceneLibrary.GetLevelStateById(id);
            if (sceneData != null)
            {
                SceneManager.LoadScene(id);
            } else
            {
                SceneManager.LoadScene(0);
            }
        }
        public void SaveSceneById(LevelState stateData)
        {
            _sceneLibrary.SaveLevelState(stateData);
        }
        public void LoadLevelForScene()
        {
            _sceneLibrary.CleanLevelLibrary();
           _sceneLibrary.LoadLevel();
            _sceneLibrary.OpeningLvl();
        }

        public List<LevelState> GetAllLevels() => _sceneLibrary.StateLevels;
        public void OpenNextLvl(string CurrentLevelId) 
        {
            _sceneLibrary.OpenNextLevel(CurrentLevelId);
        }
        public void OpenLvlById(string id)
        {
            _sceneLibrary.OpenNextLevel(id);
        }
    }
}
