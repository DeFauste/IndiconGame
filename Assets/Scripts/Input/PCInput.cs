using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Input
{
    public class PCInput : IGamePlayInput
    {
        PlayerInput _inputActions;
        public event Action OnJump;
        public event Action OnInteract;

        [Inject]
        public PCInput(PlayerInput inputActions)
        {
            _inputActions = inputActions;
            _inputActions.PCGameplay.Jump.performed += _ => Jump();
            _inputActions.PCGameplay.F.performed += _ => OnInteracrt();
        }
        private void Jump()
        {
            OnJump?.Invoke();
        }

        private void OnInteracrt()
        {
            OnInteract?.Invoke(); 
        }

        public Vector2 HorizontalDirection() => _inputActions.PCGameplay.Moveble.ReadValue<Vector2>();

        ~PCInput()
        {
            _inputActions.PCGameplay.Jump.performed -= _ => Jump();
            _inputActions.PCGameplay.F.performed -= _ => OnInteracrt();
        }
    }
}
