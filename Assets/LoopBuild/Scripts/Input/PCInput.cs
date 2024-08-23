using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Input
{
    public class PCInput : IGamePlayInput
    {
        PlayerInput _inputActions;
        public event Action OnJump;
        public event Action<bool> OnInteract;

        [Inject]
        public PCInput(PlayerInput inputActions)
        {
            _inputActions = inputActions;
            _inputActions.PCGameplay.Jump.performed += _ => Jump();
            _inputActions.PCGameplay.F.started += _ => OnInteracrt(true);
            _inputActions.PCGameplay.F.canceled += _ => OnInteracrt(false);
        }
        private void Jump()
        {
            OnJump?.Invoke();
        }

        private void OnInteracrt(bool state)
        {
            OnInteract?.Invoke(state); 
        }

        public Vector2 HorizontalDirection() => _inputActions.PCGameplay.Moveble.ReadValue<Vector2>();

        ~PCInput()
        {
            _inputActions.PCGameplay.Jump.performed -= _ => Jump();
            _inputActions.PCGameplay.F.started -= _ => OnInteracrt(true);
            _inputActions.PCGameplay.F.canceled -= _ => OnInteracrt(false);
        }
    }
}
