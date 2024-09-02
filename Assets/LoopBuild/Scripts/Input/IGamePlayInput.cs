using System;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public interface IGamePlayInput
    {
        public Vector2 HorizontalDirection();
        public event Action OnJump;
        public event Action<bool> OnInteract;
        public void IsMove(bool state);
    }
}
