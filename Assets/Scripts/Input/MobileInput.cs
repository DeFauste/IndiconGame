using System;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public class MobileInput : IGamePlayInput
    {
        public event Action OnJump;
        public event Action<bool> OnInteract;

        public Vector2 HorizontalDirection()
        {
            float x = SimpleInput.GetAxis("Horizontal");
            return new Vector2(x, 0);
        }
    }
}
