using Assets.Scripts.Level.Data;
using Assets.Scripts.Services;
using UnityEngine;

namespace Assets.Scripts.Level
{
    public class Testing : MonoBehaviour
    {
        public LevelLibrary LevelLibrary;
        public LevelService LevelService;
        private void Start()
        {
            LevelService = new LevelService(LevelLibrary);
            LevelService.LoadLevelFromManagerScene();
        }
    }
}
