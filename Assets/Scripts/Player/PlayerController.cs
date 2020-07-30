﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Health = 100;
    public float MoveSpeed = 10f;
    public float JumpForce = 10f;
    public float Damage = 10f;
    public float AttackSpeed = 2f;

    private AttackTrackingPlayer _attackTracking;
    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float _timer = 0;
    private bool _isDead = false;
    private bool _isFight = false;
    private float _moveSpeed;
    private ComboAttacks _comboAttacks = ComboAttacks.Attack1;

    private enum ComboAttacks
    {
        Attack1,
        LastAttack
    }


    void Start()
    {
        _moveSpeed = MoveSpeed;
        _attackTracking = GetComponentInChildren<AttackTrackingPlayer>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        if (Health <= 0)
            Kill();
        if(MoveSpeed > 0)
            Attack();
    }

    void FixedUpdate()
    {
        print("Player is fight = " + _isFight);
        if (!_isDead && MoveSpeed > 0)
            Move();
    }

    public void TakeDamage(float damage)
    {
        _animator.SetTrigger("takeHit");
        Health -= damage;
    }

    public bool IsLookLeft()
    {
        return _spriteRenderer.flipX;
    }

    public bool IsLookRight()
    {
        return !_spriteRenderer.flipX;
    }

    public void StartStand()
    {
        MoveSpeed = 0;
    }

    public void StopStand()
    {
        MoveSpeed = _moveSpeed;
    }

    public bool IsDead()
    {
        return _isDead;
    }

    public void StartFight()
    {
        _isFight = true;
    }

    public void StopFight()
    {
        _isFight = false;
    }

    public bool IsFight()
    {
        return _isFight;
    }
    private void Move()
    {
        Run();

        Jump();
    }

    private void Run()
    {
        if (Input.GetButton("Horizontal"))
        {
            float move = Input.GetAxis("Horizontal");

            _rigidbody2D.velocity = new Vector2(move * MoveSpeed, _rigidbody2D.velocity.y);

            _spriteRenderer.flipX = _rigidbody2D.velocity.x < 0.0f;
        }
        else
        {
            _animator.SetFloat("speed", 0.0f);
        }

        if (_rigidbody2D.velocity.x != 0)
            _animator.SetFloat("speed", MoveSpeed);
    }

    private void Jump()
    {
        if (_rigidbody2D.velocity.y == 0)
            _animator.SetTrigger("idle");
        if (_rigidbody2D.velocity.y < 0)
        {
            _animator.SetFloat("yVelocity", _rigidbody2D.velocity.y);
        }

        if (Input.GetButton("Jump"))
        {
            if (_rigidbody2D.velocity.y == 0)
                _rigidbody2D.AddForce(transform.up * (JumpForce), ForceMode2D.Impulse);

            _animator.SetFloat("yVelocity", _rigidbody2D.velocity.y);
        }
    }

    private void Attack()
    {
        if (_timer >= 1 / AttackSpeed)
        {
            if (Input.GetButtonDown("Fire1") && _comboAttacks == ComboAttacks.Attack1)
            {
                _attackTracking.Attack();
                _animator.SetTrigger("attack1");
                CorrectQueueComboAttacks();
                return;
            }

            if (Input.GetButtonDown("Fire1") && _comboAttacks == ComboAttacks.LastAttack)
            {
                _attackTracking.Attack();
                _animator.SetTrigger("attack2");
                CorrectQueueComboAttacks();
                _timer = 0;
                return;
            }
        }
    }

    private void CorrectQueueComboAttacks()
    {
        switch (_comboAttacks)
        {
            case ComboAttacks.Attack1:
                _comboAttacks = ComboAttacks.LastAttack;
                break;
            case ComboAttacks.LastAttack:
                _comboAttacks = ComboAttacks.Attack1;
                break;
        }
    }

    private void Kill()
    {
        _isDead = true;
        _animator.SetTrigger("death");
        Destroy(_attackTracking);
    }
}