using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float moveSpeed;
    private float _moveDir;

    private Rigidbody2D _rigidBody;
    private bool _jumpPressed;
    private float _jumpYVel;
    private Vector2 _modeVel;
    private bool _isOnSurface;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
        HandleJump();
    }

    private void HandleJump()
    {
        if (_jumpPressed && _isOnSurface)
        {
            _jumpYVel = CalculateJumpVel(_jumpHeight);
            _jumpPressed = false;

            _modeVel = _rigidBody.velocity;
            _modeVel.y = _jumpYVel;
            _rigidBody.velocity = _modeVel;
        }
    }

    private void Move()
    {
        _modeVel = _rigidBody.velocity;
        _modeVel.x = _moveDir * moveSpeed * Time.fixedDeltaTime;
        _rigidBody.velocity = _modeVel;
    }

    private float CalculateJumpVel(float height)
    {/*
        return MathF.Sqrt((-2 * _rigidBody.gravityScale * Physics2D.gravity.y * height));*/
        return MathF.Sqrt((-2 * _rigidBody.gravityScale * Physics2D.gravity.y * height));
    }

    void GetInput()
    {
        _moveDir = Input.GetAxis("Horizontal");
        _jumpPressed |= Input.GetKeyDown(KeyCode.Space);

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _isOnSurface = true;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isOnSurface = false;
    }
}
