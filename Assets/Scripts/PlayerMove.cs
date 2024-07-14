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
    [SerializeField] private Rigidbody2D _rb;
    private PlayerInput input;
    private Vector2 direction = Vector2.zero;
    #region Jump
    [SerializeField, Range(0, 200)] private float _jumpForce = 4f;
    private bool _isJump = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private float fallSpeed = 5;
    private Vector2 vecGravity;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        InitComponent();
        InitInput();
    }
    
    private void InitComponent()
    {
        if(_rb == null) _rb = GetComponent<Rigidbody2D>();
        vecGravity = new Vector2(0, -Physics2D.gravity.y);
    }
    private void InitInput()
    {
        input = new PlayerInput();
        input.Enable();
    }

    private Vector2 GetDirection()
    {
        return input.Gameplay.Moveble.ReadValue<Vector2>(); ;
    }
    private void Update()
    {
        _isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(1f, 0.3f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        _isJump = input.Gameplay.Jump.inProgress;
        direction = GetDirection();
    }
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(direction.x * _speed, _rb.velocity.y);
        if (_isJump)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
        }
        if (_rb.velocity.y < 0)
        {
            _rb.velocity -= vecGravity * fallSpeed * Time.fixedDeltaTime;
        }
    }
}
