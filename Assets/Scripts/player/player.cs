using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class player : MonoBehaviour
{
    [SerializeField] private float _jumpHeight;
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform weapon;
    private float _moveDir;

    private Rigidbody2D _rigidBody;
    private bool _jumpPressed;
    private float _jumpYVel;
    private Vector2 _modeVel;
    private bool _isOnSurface;
    private bool _isDead;

    private Animator _animator;
    private Health _health;


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _health = GetComponent<Health>(); 

       
        //_health.OnHealthChanged += HandleHealthChanged;
        //_health.OnDeath += HandleDeath;
    }


    private void Update()
    {
        if (_isDead) return;

        GetInput();
        UpdateAnimations();
        Suicide();
    }

    private void FixedUpdate()
    {
        if (_isDead) return;


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

            _animator.SetBool("isJumping", true);
        }
    }

    private void Move()
    {
        _modeVel = _rigidBody.velocity;
        _modeVel.x = _moveDir * moveSpeed * Time.fixedDeltaTime;
        _rigidBody.velocity = _modeVel;

        _animator.SetBool("isRunning", MathF.Abs(_moveDir) > 0);
    }

    private float CalculateJumpVel(float height)
    {
        return MathF.Sqrt((-2 * _rigidBody.gravityScale * Physics2D.gravity.y * height));
    }

    private void Suicide()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _health.TakeDamage(_health.MaxHealth);
            _isDead = true;
            _animator.SetBool("isDead", true);

        }
    }

    private void HandleDeath()
    {
        _isDead = true;
        _animator.SetBool("isDead", true);
        
        _rigidBody.velocity = Vector2.zero; 
        
    }


    private void HandleHealthChanged(int currentHealth)
    {
        // TODO: UI change
    }

    void GetInput()
    {
        _moveDir = Input.GetAxis("Horizontal");
        _jumpPressed |= Input.GetKeyDown(KeyCode.Space);
        
    }

    private void UpdateAnimations()
    {
        _animator.SetBool("isDead", _isDead);
        _animator.SetBool("isOnSurface", _isOnSurface);
        
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _isOnSurface = true;
        _animator.SetBool("isJumping", false); 
    }


    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    public void Heal(int amount)
    {
        _health.Heal(amount);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        _isOnSurface = false;
    }

    private void RotateWeaponTowardsCursor()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - (Vector2)weapon.position).normalized;
        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        weapon.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
