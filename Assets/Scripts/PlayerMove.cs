using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
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
    [SerializeField]private float wallJumpDirectionForce = 5;
    private float wallJumpDirection = 0;
    [SerializeField] public bool HaveWallJunping = false;
    #endregion

    #region Wall Slide
    [SerializeField] private Transform leftWallCheck;
    [SerializeField] private Transform rightWallCheck;
    [SerializeField] private LayerMask wallLayer;
    private bool isWallSilding;
    [SerializeField] private float wallSlidingSpeed = 2f;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
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
        input.Gameplay.Jump.performed  += _ => Jump();
    }

    private Vector2 GetDirection()
    {
        return input.Gameplay.Moveble.ReadValue<Vector2>(); ;
    }
    private void Update()
    {
        _isGrounded = isGrounded();
        direction = GetDirection();
    }

    private bool isGrounded() => Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.7f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundLayer);
    private bool isLeftWall() => Physics2D.OverlapCapsule(leftWallCheck.position, new Vector2(0.7f, 0.3f), CapsuleDirection2D.Vertical, 0, wallLayer);
    private bool isRightWall() => Physics2D.OverlapCapsule(rightWallCheck.position, new Vector2(0.7f, 0.3f), CapsuleDirection2D.Vertical, 0, wallLayer);

    private void FixedUpdate()
    {
        Move();
        //GravityFall();
        WallSlide();
    }

    private void WallSlide()
    {
        if(!_isGrounded && (isLeftWall() || isRightWall()) && _rb.velocity.x != 0)
        {
            isWallSilding = true;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Clamp(_rb.velocity.y, -wallSlidingSpeed , float.MaxValue));
            wallJumpDirection = isLeftWall() ? wallJumpDirectionForce : -wallJumpDirectionForce;
        }else
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
        if(!isWallSilding)
        _rb.velocity = new Vector2(direction.x * _speed, _rb.velocity.y);
    }

    private void Jump()
    {
        if(HaveWallJunping && isWallSilding)
        {
            Debug.Log($"!{wallJumpDirection}!");
            _rb.velocity = new Vector2(wallJumpDirection, _jumpForce);
        }
        else if (_isGrounded) {
           _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }else if(_rb.velocity.y != 0 && !_isGrounded && _jumpCount < maxJumpCount)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _jumpCount++;
         }
        
        if(_isGrounded) 
        {
            _jumpCount = 1;
        }
    }
}
