using Assets.Scripts.Input;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour, IPlayerProperty
    {
        private Rigidbody2D _rb;
        private IGamePlayInput _gamePlayInput;
        private Animator _animator;

        private readonly int JumpHash = Animator.StringToHash("IsJumping");
        private readonly int RunHash = Animator.StringToHash("HasXVelocity");

        #region MOVE
        [SerializeField] private float SpeedMove = 10f;
        [SerializeField] private float wallSlidingSpeed = 5f;
        #endregion

        #region JUMP
        [SerializeField] private float JumpForce = 10f;
        [SerializeField] private LayerMask ObstacleLayers;
        [SerializeField] Collider2D GroundCheck;
        private bool _isGrounded;
        private int JumpCount = 0;
        private int JumpAddCount { get; set; } = 1;
        private bool _haveDoubleJump = false;
        #endregion

        #region Wall
        [SerializeField] private float JumpWallX = 50f;
        [SerializeField] Collider2D WallCheck;
        private bool _isWall;
        private bool _isWallSilding;
        private bool _haveSliding = false;
        #endregion

        #region Property
        public bool HaveDoubleJump { get => _haveDoubleJump; set => _haveDoubleJump = value; }
        public bool HaveSliding { get => _haveSliding; set => _haveSliding = value; }
        public float ChangeJumpForce
        {
            get => JumpForce;
            set
            {
                if (value > 0) JumpForce = value;
            }
        }

        public float ChangeMoveSpeed { get => SpeedMove; set => SpeedMove = value; }
        #endregion

        [Inject]
        public void Construct(IGamePlayInput gamePlayInput)
        {
            _gamePlayInput = gamePlayInput;
        }

        void Start()
        {
            InitProperty();
            _gamePlayInput.OnJump += Jump;
            _animator = gameObject.GetComponent<Animator>();
        }

        private void InitProperty()
        {
            _rb = GetComponent<Rigidbody2D>();

        }

        private void Update()
        {
            AnimationState();
        }

        private void FixedUpdate()
        {
            Move();
            CheckObstacle();
            WallSlide();
            FlipObject();
        }

        private bool IsRunning()
        {
            return Mathf.Abs(_gamePlayInput.HorizontalDirection().x) > 0.01f;
        }

        private void AnimationState()
        {
            _animator.SetBool(JumpHash, !_isGrounded);
            _animator.SetBool(RunHash, IsRunning() && _isGrounded);
        }

        private void CheckObstacle()
        {
            _isWall = WallCheck.IsMyTouchingLayers(ObstacleLayers);
            _isGrounded = GroundCheck.IsMyTouchingLayers(ObstacleLayers);
        }

        private void Move()
        {
            if (!_isWallSilding)
            {
                _rb.velocity = new Vector2(SpeedMove * _gamePlayInput.HorizontalDirection().x, _rb.velocity.y);
            }

        }

        private void FlipObject()
        {
            if (_rb.velocity.x != 0 && !_isWallSilding)
            {
                gameObject.transform.localScale = _rb.velocity.x > 0 ?
                    new Vector3(-Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, 1) :
                    new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, 1);
            }
        }

        private void WallSlide()
        {
           if (_haveSliding && !_isGrounded && _isWall && IsRunning())
           {
               _isWallSilding = true;
               _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
           }
           else
           {
                _isWallSilding = false;
           }
        }
        private void Jump()
        {
            float bustJumpOfScale = Mathf.Abs(gameObject.transform.localScale.x);
            float directionJump = _rb.velocity.x;
            Vector2 jumpVector = new Vector2(directionJump, 1+ bustJumpOfScale);
            if (_isWall && !_isGrounded)
            {
                directionJump = gameObject.transform.localScale.x * JumpWallX;
            }
            if (_isWallSilding &&_isWall || _isGrounded)
            {
                //_rb.velocity = new Vector2(directionJump, JumpForce);
                _rb.AddForce(jumpVector * JumpForce, ForceMode2D.Impulse);
            }
            if (HaveDoubleJump && JumpCount < JumpAddCount && !_isGrounded)
            {
                //_rb.velocity = new Vector2(directionJump, JumpForce);
                _rb.AddForce(jumpVector * (JumpForce / 2), ForceMode2D.Impulse);
                JumpCount++;
            }else if(_isGrounded)
            {
                JumpCount = 0;
            }
        }

        private void OnDestroy()
        {
            _gamePlayInput.OnJump -= Jump;
        }

    }
}
