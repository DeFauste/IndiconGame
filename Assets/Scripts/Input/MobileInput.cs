using System;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public class MobileInput : MonoBehaviour, IGamePlayInput
    {
        public event Action OnJump;
        public event Action<bool> OnInteract;

        private string _jumpButton = "Jump";
        private string _squeezeButton = "Squeeze";
       
        public Vector2 HorizontalDirection()
        {
            float x = SimpleInput.GetAxis("Horizontal");
            return new Vector2(x, 0);
        }

        private void Update()
        {
            if (SimpleInput.GetButtonDown(_jumpButton))
            {
                OnJump?.Invoke();
            }
            if (SimpleInput.GetButtonDown(_squeezeButton))
            {
                OnInteract?.Invoke(true);
            }
            else if (SimpleInput.GetButtonUp(_squeezeButton))
            {
                OnInteract?.Invoke(true);
            }
        }
    }
}
