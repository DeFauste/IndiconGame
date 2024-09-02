using Cinemachine;
using UnityEngine;
using Zenject;

namespace Assets.LoopBuild.Scripts.Player
{
    public class CinemachineInject : MonoBehaviour
    {
        [SerializeField] CinemachineVirtualCamera virtualCamera;

        [Inject]
        public void Constructor(Transform plyerPoint)
        {
            virtualCamera.Follow = plyerPoint;
        }
    }
}
