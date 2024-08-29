using Assets.LoopBuild.Scripts.Manager;
using UnityEngine;

namespace Assets.LoopBuild.Scripts
{
    public class Test: MonoBehaviour
    {
        private void Start()
        {
           SceneManagerInfo sceneManagerInfo = new SceneManagerInfo();
           sceneManagerInfo.LoadLevel();
           foreach(var item in sceneManagerInfo.AllScenesInfo)
           {
               Debug.Log($"{item.name} {item.state}");
           }
            sceneManagerInfo.SaveProgress();

        }
    }
}
