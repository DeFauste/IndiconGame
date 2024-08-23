using Assets.Scripts.Input;
using UnityEngine;

namespace Assets.Scripts.NPC
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class MoveForTouch : MonoBehaviour
    {
        [SerializeField] private LayerMask IsTouschLayer;
        [SerializeField] private Collider2D senceColider;
        [SerializeField] private float _speedMove;
        [SerializeField] private bool RightMove = true;
        private int directionMove = 1;
        [SerializeField] private Rigidbody2D _rb;

        private void Start()
        {
            if (!RightMove)
            {
                directionMove = -1;
                SwapDirectionObject();
            }
            if(_rb == null) _rb = GetComponent<Rigidbody2D>();
        }
        private void FixedUpdate()
        {
            if (senceColider.IsMyTouchingLayers(IsTouschLayer))
            {
                directionMove *= -1;
                SwapDirectionObject();
            }
            _rb.velocity = Vector2.right * _speedMove * directionMove;
        }

        private void SwapDirectionObject()
        {
            gameObject.transform.localScale =
                   new Vector3(gameObject.transform.localScale.x * -1,
                   gameObject.transform.localScale.y,
                   gameObject.transform.localScale.z);
        }
    }
}
