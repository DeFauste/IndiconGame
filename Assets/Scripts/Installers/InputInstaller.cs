using Assets.Scripts.InputSystem;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class InputInstaller : MonoInstaller
    {
        PlayerInput playerInput;
        IGamePlayInput gamePlayInput;
        [SerializeField] private MobileInput mobileInput;
        public override void InstallBindings()
        {
            Activate();
            Container.Bind<PlayerInput>().FromInstance(playerInput).AsSingle();
            if(SystemInfo.deviceType == DeviceType.Desktop)
            {
                Container.Bind<IGamePlayInput>().To<PCInput>().AsSingle();
            }
            else if(SystemInfo.deviceType == DeviceType.Handheld)
            {
                Container.Bind<IGamePlayInput>().FromInstance(mobileInput).AsSingle();
            }
        }
        private void Activate()
        {
            playerInput = new PlayerInput();
            playerInput.Enable();
        }
    }
}