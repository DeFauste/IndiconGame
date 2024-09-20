using Assets.LoopBuild.Scripts.Saver;
using Assets.LoopBuild.Scripts.Saver.JSON;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace Assets.LoopBuild.Scripts.Scene
{
    [Serializable]
    public class SceneInfo
    {
        [JsonProperty("Name")]
        public string name;
        [JsonProperty("State")]
        public bool state;
    }
    public class SceneManagerInfo
    {
        public SceneInfo[] AllScenesInfo;
        private int currentLvl = 0;
        ISavedata savedata = new SaverJson();
        string saveFile = "SaveProgress.json";

        public SceneManagerInfo()
        {
            LoadLevel();
        }
        public void LoadLevel()
        {
            LoadFile();
            if(AllScenesInfo == null || AllScenesInfo.Length == 0)
            {
                DefauldLoad();
                Debug.Log("DEFAULT");
            }
        }

        private void LoadFile()
        {
            try
            {
                string assetsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets\\Resources", saveFile);
                AllScenesInfo = savedata.Load<SceneInfo>(assetsFolderPath);
            }catch (Exception ex)
            {
                Debug.LogException(ex);
            }

        }

        private void DefauldLoad()
        {
            int length = EditorBuildSettings.scenes.Length;
            AllScenesInfo = new SceneInfo[length];
            for(int i = 0; i < length; i++)
            {
                string scenePath = EditorBuildSettings.scenes[i].path;
                string sceneName = Path.GetFileNameWithoutExtension(scenePath);
                var info = new SceneInfo();
                info.name = sceneName;
                info.state = false;
                AllScenesInfo[i] = info;
            }
        }

        public void SaveProgress()
        {
            string assetsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Assets\\Resources", saveFile);
            savedata.Save(AllScenesInfo, $"{assetsFolderPath}");
        }

        public void StartLvlForName(string Name)
        {
            SceneManager.LoadScene(Name);
            for(int i = 0; i < AllScenesInfo.Length; i++)
            {
                if (AllScenesInfo[i].name == Name)
                {
                    currentLvl = i;
                }
            }
        }
        public void StartLvlForIndex(int index)
        {
            var name = AllScenesInfo[index].name;
            SceneManager.LoadScene(name);
        }
        public int GetIndexLvl() => currentLvl;
        public void CheckedLvl(string Name)
        {
            AllScenesInfo.First(x=> x.name == Name).state = true;

        }

        public void NextLvl()
        {
            currentLvl++;
            SceneManager.LoadScene(AllScenesInfo[currentLvl].name);
        }
    }

}
