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
        }
    }
}
