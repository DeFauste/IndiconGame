using Assets.Scripts.Player;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    internal class LocationInstaller : MonoInstaller
    {
        public Transform startPoint;
        public GameObject heroPref;
        public override void InstallBindings()
        {
            GameObject heroInst = Container.InstantiatePrefab(heroPref, startPoint.position, Quaternion.identity, null);
            IPlayerProperty playerProperty = heroInst.GetComponent<PlayerController>();
            Container.Bind<IPlayerProperty>().FromInstance(playerProperty).AsSingle();
            Container.Bind<Transform>().FromInstance(heroInst.transform);
        }
    }
}
