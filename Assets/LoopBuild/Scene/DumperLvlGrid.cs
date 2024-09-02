using Assets.LoopBuild.Scripts.Scene;
using Microsoft.Unity.VisualStudio.Editor;
using SimpleInputNamespace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.LoopBuild.Scene
{
    public class DumperLvlGrid : MonoBehaviour
    {
        private SceneManagerInfo _sceneManagerInfo;
        [SerializeField]private GameObject canvas;
        [SerializeField]private GameObject grid; 

        [Inject]
        public void Construct(SceneManagerInfo sceneManagerInfo)
        {
            _sceneManagerInfo = sceneManagerInfo;
             Debug.Log("Dump");
        }

        private void Start()
        {
            DindGrid();
        }

        private void DindGrid()
        {
            if (canvas == null || _sceneManagerInfo == null || grid == null)
            {
                Debug.Log("Nuuul");

                return;
            }

            foreach (var item in _sceneManagerInfo.AllScenesInfo)
            {
                if(item.name == "MainMenu") continue;
                var obj = Instantiate(canvas);
                var bt = obj.GetComponent<Button>();
                var txt = obj.GetComponentInChildren<TextMeshProUGUI>();
                txt.text = item.name;
                obj.transform.SetParent(grid.transform, false);
            }
        }
    }
}
