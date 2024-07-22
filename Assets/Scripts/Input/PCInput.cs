using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Input
{
    public class PCInput : IGamePlayInput
    {
        public event Action OnJump;
        public event Action OnInteract;

        public Vector2 HorizontalDirection()
        {
            throw new NotImplementedException();
        }
    }
}
