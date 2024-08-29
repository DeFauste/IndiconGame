using Assets.LoopBuild.Scripts.Saver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


namespace Assets.LoopBuild.Scripts.Manager
{
    public class SceneManagerInfo
    {
        public SceneInfo[] AllScenesInfo;
        ISavedata savedata = new SaverJson();
        string saveFile = "SaveProgress.json";
        public void LoadLevel()
        {
           // LoadFile();
            if(AllScenesInfo == null )
            {
                DefauldLoad();
            }
        }

        private void LoadFile()
        {
            try
            {
                string assetsFolderPath = Path.Combine(Application.persistentDataPath, saveFile);
                AllScenesInfo = savedata.Load<SceneInfo[]>(assetsFolderPath);
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
            Debug.Log("Default");
        }

        public void SaveProgress()
        {
            string assetsFolderPath = Path.Combine(Application.persistentDataPath, saveFile);
            Debug.Log(AllScenesInfo.Length);
            savedata.Save(AllScenesInfo, $"{assetsFolderPath}");
        }
    }
    [Serializable]
    public class SceneInfo
    {
        [JsonProperty("Name")]
        public string name;
        [JsonProperty("State")]
        public bool state;
    }
}
