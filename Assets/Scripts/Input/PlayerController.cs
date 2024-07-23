using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Input
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private Rigidbody2D _rb;
        private IGamePlayInput _gamePlayInput;

        #region MOVE
        [SerializeField] private float SpeedMove = 10f;
        #endregion

        #region JUMP
        [SerializeField] private float JumpForce = 10f;
        [SerializeField] private LayerMask ObstacleLayers;
        [SerializeField] Collider2D WallCheck;
        [SerializeField] Collider2D GroundCheck;
        [SerializeField] private bool _isGrounded;
        [SerializeField] private bool _isWall;
        #endregion

        [Inject]
        public void Construct(IGamePlayInput gamePlayInput)
        {
            Debug.Log("Inst New Player Input");
            _gamePlayInput = gamePlayInput;
        }

        void Start()
        {
            InitProperty();
            _gamePlayInput.OnJump += Jump;
            _gamePlayInput.OnInteract += Interact;
        }

        private void InitProperty()
        {
            _rb = GetComponent<Rigidbody2D>();

        }
        private void FixedUpdate()
        {
            Move();
            CheckObstacle();
        }

        private void CheckObstacle() 
        {
            _isWall = WallCheck.IsMyTouchingLayers(ObstacleLayers);
            _isGrounded = GroundCheck.IsMyTouchingLayers(ObstacleLayers);
        }
        
        private void Move()
        {
            _rb.velocity = new Vector2(SpeedMove * _gamePlayInput.HorizontalDirection().x, _rb.velocity.y);
            if (_rb.velocity.x != 0)
            {
                gameObject.transform.localScale = _rb.velocity.x > 0 ? new Vector3(-1, 1, 1) : new Vector3(1, 1, 1);
            }
        }
        private void Jump()
        {
            float directionJump = _rb.velocity.x;
            if (_isWall && !_isGrounded)
            {
                directionJump = -gameObject.transform.localScale.x * SpeedMove;
                Debug.Log($"Direction Jump: {directionJump}");
            }
            if (_isWall || _isGrounded)
            {
                _rb.velocity = new Vector2(directionJump, JumpForce);
            }
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
