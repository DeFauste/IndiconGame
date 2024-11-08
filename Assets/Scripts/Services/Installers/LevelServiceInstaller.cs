using Assets.Scripts.Level.Data;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Services.Installers
{
    public class LevelServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var levelLibrary = Resources.Load<LevelLibrary>("LevelLibrary");
            if (levelLibrary == null)
            {
                Debug.LogError($"levelLibrary not found in resources");
                return;
            }
            var levelService = new LevelService(levelLibrary);
            Container.Bind<LevelService>().FromInstance(levelService).AsSingle();
        }
    }
}
