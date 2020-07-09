using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 10f;

    public float JumpForce = 0.001f;

    private Rigidbody2D _rigidbody2D;

    private bool _isFacingRight;

    private Animator _animator;

    private SpriteRenderer _spriteRenderer;


    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Attack();
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
        else
        {
            _animator.SetFloat("yVelocity", _rigidbody2D.velocity.y);
        }
        
        if (Input.GetButton("Jump"))
        {
            if (_rigidbody2D.velocity.y == 0)
            {
                _rigidbody2D.AddForce(transform.up * JumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void Attack()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            print("fire");
            _animator.SetTrigger("attack1");
        }
        _animator.SetTrigger("idle");
    }
}
