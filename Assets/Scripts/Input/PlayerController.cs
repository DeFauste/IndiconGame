using UnityEngine;
using Zenject;

namespace Assets.Scripts.Input
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] 

        private IGamePlayInput _gamePlayInput;

        [Inject]
        public void Construct(IGamePlayInput gamePlayInput)
        {
            Debug.Log("Inst New Player Input");
            _gamePlayInput = gamePlayInput;
        }

        void Start()
        {
            _gamePlayInput.OnJump += Jump;
            _gamePlayInput.OnInteract += Interact;

        }

        private void Move()
        {
            
        }
        private void Jump()
        {
            Debug.Log("Jump");
        }

        private void Interact()
        {
            Debug.Log("Interact");
        }

        private void OnDestroy()
        {
            _gamePlayInput.OnJump -= Jump;
        }
    }
}
