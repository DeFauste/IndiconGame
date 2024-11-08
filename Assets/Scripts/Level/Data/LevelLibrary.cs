using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Level.Data
{
    [CreateAssetMenu(fileName = "NewSceneLibrary", menuName = "SceneLibrary/SceneLibrary")]
    public class LevelLibrary : ScriptableObject
    {
        public List<LevelState> StateLevels = new List<LevelState>();
        public LevelState GetLevelStateById(string id)
        {
            var sceneStates = StateLevels.Find(scene => scene.Id == id);
            return sceneStates;
        }

        public void SaveLevelState(LevelState sceneState)
        {
            var date = StateLevels.Find(x => x.Id == sceneState.Id);
            date = sceneState;
        }
        public void LevelIsFinish(LevelState levelState)
        {
            foreach (var level in StateLevels)
            {
                if(level.Id == levelState.Id)
                {
                    level.IsFinish = true;
                    break;
                }
            }
        }
        public void LoadLevelsFromManagerScene()
        {
            var listLvl = GetAllSceneNames();

            foreach (var sceneName in listLvl)
            {
                LevelState newLevel = new LevelState();
                newLevel.Id = sceneName;
                newLevel.IsFinish = false;
                newLevel.IsOpen = false;
                var findLevel = GetLevelStateById(sceneName);

                if (findLevel == null)
                {
                    StateLevels.Add(newLevel);
                }
            }
        }
        public void OpeningLvl()
        {
            for (int i = 0; i < StateLevels.Count; i++)
            {
                if (i == 0)
                {
                    StateLevels[i].IsOpen = true;
                    StateLevels[i].IsFinish = false;
                } else if (StateLevels[i-1].IsFinish)
                {
                    StateLevels[i].IsOpen = true;
                }
            }
        }
        private List<string> GetAllSceneNames()
        {
            List<string> sceneNames = new List<string>();
            int sceneCount = SceneManager.sceneCountInBuildSettings;

            for (int i = 1; i < sceneCount; i++)
            {
                string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
                string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
                sceneNames.Add(sceneName);
            }

            return sceneNames; 
        }

        public void CleanLevelLibrary()
        {
            StateLevels.Clear();
        }

        public void OpenLevelById(string id)
        {
            for (int i = 0; i < StateLevels.Count; i++)
            {
                if (StateLevels[i].Id == id && i < StateLevels.Count)
                {
                    StateLevels[i].IsOpen = true;
                    StateLevels[i].IsFinish = false;
                }
            }
        }

        public LevelState GetNextLevel(string currnetIdLevel)
        {
            LevelState level = null;
            Debug.Log(currnetIdLevel);
            for (int i = 0; i < StateLevels.Count; i++)
            {
                if (StateLevels[i].Id == currnetIdLevel && (i + 1) < StateLevels.Count)
                {
                    level = StateLevels[i+1];
                    level.IsOpen = true;
                    Debug.Log(level.Id  );

                    break;
                }
            }
            return level;
        }
    }
}

