using Assets.LoopBuild.Scripts.Scene;
using Zenject;

namespace Assets.LoopBuild.Scripts.Installers
{
    public class GlobalInstaller: MonoInstaller
    {
        public SceneManagerInfo SceneManagerInfo;
        public override void InstallBindings()
        {
            Inits();
            Container.Bind<SceneManagerInfo>().FromInstance(SceneManagerInfo).AsSingle();
        }
        public void Inits()
        {
            SceneManagerInfo = new SceneManagerInfo();
            SceneManagerInfo.SaveProgress();

        }
    }
}
