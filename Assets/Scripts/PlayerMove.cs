using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField, Range(0, 20)] private float _speed = 4f;
    private Rigidbody2D _rb;
    private PlayerInput input;
    private Vector2 direction = Vector2.zero;
    #region Jump
    [SerializeField, Range(0, 200)] private float _jumpForce = 4f;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private float fallSpeed = 5;
    private Vector2 vecGravity;
    [SerializeField] private int maxJumpCount = 2;
    private int _jumpCount = 1;
    [SerializeField] private float wallJumpDirectionForce = 5;
    private float wallJumpDirection = 0;
    #endregion

    #region Wall Slide
    [SerializeField] private Transform leftWallCheck;
    [SerializeField] private Transform rightWallCheck;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private bool isWallSilding;
    [SerializeField] private float wallSlidingSpeed = 2f;

    [SerializeField] private bool _isLeftWall = false;
    [SerializeField] private bool _isRightWall = false;

    #endregion

    #region Effects
    ParticleSystem splash;
    public AudioSource audioSource;
    public AudioClip jump, dive;
    #endregion

    #region Animation
    private Animator animator;
    bool isLookingRight = false;
    #endregion

    #region Player Property
    [SerializeField] public bool HaveDoubleJump = false;
    [SerializeField] public bool HaveWallJumping = false;
    public float JumpForce { get => _jumpForce; set
        {
            _jumpForce = value;
        }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        splash = GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();

        InitComponent();
        InitInput();
    }

    private void InitComponent()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody2D>();
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }
    private void InitInput()
    {
        input = new PlayerInput();
        input.Enable();
        input.Gameplay.Jump.performed += _ => Jump();
    }

    private Vector2 GetDirection()
    {
        return input.Gameplay.Moveble.ReadValue<Vector2>(); ;
    }
    private void Update()
    {
        _isGrounded = isGrounded();

        _isLeftWall = isLeftWall();
        _isRightWall = isRightWall();

        direction = GetDirection();

        animator.SetBool("IsJumping", !_isGrounded);
        animator.SetFloat("XVelocity", Math.Abs(direction.x * _speed));

        if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.R))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    private bool isGrounded() => Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.3f, 0.5f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    private bool isLeftWall() => Physics2D.OverlapCapsule(leftWallCheck.position, new Vector2(0.7f, 0.3f), CapsuleDirection2D.Vertical, 0, wallLayer);
    private bool isRightWall() => Physics2D.OverlapCapsule(rightWallCheck.position, new Vector2(0.7f, 0.3f), CapsuleDirection2D.Vertical, 0, wallLayer);

    private void FixedUpdate()
    {
        FlipCheck();
        Move();
        //GravityFall();
        WallSlide();
    }

    private void WallSlide()
    {
        if (!_isGrounded && (isLeftWall() || isRightWall()) && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            isWallSilding = true;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            wallJumpDirection = isLeftWall() ? wallJumpDirectionForce : -wallJumpDirectionForce;
        }
        else
        {
            isWallSilding = false;
        }
    }
    private void GravityFall()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.velocity -= vecGravity * fallSpeed * Time.fixedDeltaTime;
        }
    }

    private void Move()
    {
        if (!isWallSilding)
        {
            _rb.velocity = new Vector2(direction.x * _speed, _rb.velocity.y);
        }
    }

    private void Jump()
    {
        if (HaveWallJumping && isWallSilding)
        {
            _rb.velocity = new Vector2(wallJumpDirection, _jumpForce);
            JumpSoundPlay();
        }
        else if (_isGrounded)
        {                                                    // Jump
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            JumpSoundPlay();
        }
        else if (_rb.velocity.y != 0 && !_isGrounded && _jumpCount < maxJumpCount && HaveDoubleJump) // doubleJump
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _jumpCount++;
            JumpSoundPlay();
        }

        if (_isGrounded)
        {
            _jumpCount = 1;
        }
    }

    private void FlipCheck()
    {
        if (Input.GetKeyDown(KeyCode.A) && isLookingRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            isLookingRight = false;
        }

        if (Input.GetKeyDown(KeyCode.D) && !isLookingRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            isLookingRight = true;
        }

        if (Input.GetKey(KeyCode.A) && isLookingRight)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            isLookingRight = false;
        }

        if (Input.GetKey(KeyCode.D) && !isLookingRight)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            isLookingRight = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var layerMask = other.gameObject.layer;
        if (layerMask == LayerMask.NameToLayer("Water"))
        {
            splash.Play();
            audioSource.clip = dive;
            audioSource.Play();
        }

    }

    private void JumpSoundPlay()
    {
        audioSource.clip = jump;
        audioSource.Play();
    }
    private void OnDestroy()
    {
        input.Gameplay.Jump.performed -= _ => Jump();
        input.Disable();

    }
}
