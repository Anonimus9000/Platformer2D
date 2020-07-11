using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Health = 100;
    public float MoveSpeed = 10f;
    public float JumpForce = 10f;
    public float Damage = 10f;
    public float AttackSpeed = 0.5f;

    private AttackTrackingPlayer _attackTracking;
    private Rigidbody2D _rigidbody2D;
    private bool _isFacingRight;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private float _timer = 0;
    private float _timeLastAttack;
    private float _periodAttack = 0.6f;
    private List<string> _arrayAttacks = new List<string>();
    

    void Start()
    {
        _attackTracking = GetComponentInChildren<AttackTrackingPlayer>();
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        Attack();
    }

    void FixedUpdate()
    {
        Move();
    }
    public void TakeDamage(float damage)
    {
        print(Health);
        print("takeHitPlayer");
        _animator.SetTrigger("takeHit");
        Health -= damage;
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
        if(_rigidbody2D.velocity.x != 0)
            _animator.SetFloat("speed", MoveSpeed);
    }

    private void Jump()
    {
        if(_rigidbody2D.velocity.y == 0)
            _animator.SetTrigger("idle");
        if(_rigidbody2D.velocity.y < 0)
        {
            _animator.SetFloat("yVelocity", _rigidbody2D.velocity.y);
        }
        
        if (Input.GetButton("Jump"))
        {
            if (_rigidbody2D.velocity.y == 0)
            {
                _rigidbody2D.AddForce(transform.up * (JumpForce ), ForceMode2D.Impulse);
            }
        }
    }

    private void Attack()
    {
        bool isLastComboAttack = false;
        if (_timer >= 1 / AttackSpeed)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                _attackTracking.Attack();
                _animator.SetTrigger("attack1");
                _timer = 0;
                _arrayAttacks.Add("Fire1");
                _timeLastAttack = Time.time;
            }
            if (Input.GetButtonDown("Fire2") && Time.time < _timeLastAttack + _periodAttack && _arrayAttacks.Last() == "Fire1")
            {
                _arrayAttacks.Add("Fire2");
                _animator.SetTrigger("attack2");
                _timer = 0;
                isLastComboAttack = true;
            }
        }

        if (isLastComboAttack)
        {
            _arrayAttacks.Clear();
        }
    }

    
}
