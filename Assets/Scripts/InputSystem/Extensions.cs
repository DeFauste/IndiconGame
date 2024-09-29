using UnityEngine;

namespace Assets.Scripts.InputSystem
{
    public static class Extensions
    {
        public static bool IsMyTouchingLayers(this Collider2D collider2D, LayerMask layerMask)
        {
            if (collider2D != null) { return collider2D.IsTouchingLayers(layerMask); }
            return false;
        }
    }
}
