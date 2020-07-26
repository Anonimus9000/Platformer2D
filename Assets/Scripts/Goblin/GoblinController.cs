using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using Assets.Scripts.Interfaces;

public class GoblinController : EnemyMob
{
    private bool _isSeePlayer = false;
    private AttackTrackingGoblin _attackTracking;
    private float _timer = 0;
    private float _moveSpeed;
    private float _nowPositionPotrol;
    private bool _isPotrolRight = false;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private PlayerController _player;
    private CapsuleCollider2D _capsuleCollider2D;

    void Awake()
    {
        _moveSpeed = MoveSpeed;
    }
    void Start()
    {
        _attackTracking = GetComponentInChildren<AttackTrackingGoblin>();
        _nowPositionPotrol = RangePotrol;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerController>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        if (Health <= 0)
            Kill();
        else if (_isSeePlayer)
            Attack(_player.gameObject);

        _timer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (Health > 0)
        {
            if (Potrol)
                EnemyPotrol();
            if (!_player.IsDead())
            {
            }

            if (MoveSpeed != 0)
                MoveToObject(_player.gameObject, 4);
        }
    }

    public override void LookLeft()
    {
        _spriteRenderer.flipX = true;
    }

    public override void LookRight()
    {
        _spriteRenderer.flipX = false;
    }

    public override void MoveToPosition(Vector3 position)
    {
        _animator.SetFloat("speed", MoveSpeed);

        if (position.x + 0.2f < gameObject.transform.position.x)
            _rigidbody2D.velocity = new Vector2(-MoveSpeed, _rigidbody2D.velocity.y);
        if (position.x + 0.2f > gameObject.transform.position.x)
            _rigidbody2D.velocity = new Vector2(MoveSpeed, _rigidbody2D.velocity.y);

        _spriteRenderer.flipX = _rigidbody2D.velocity.x < 0.0f;
    }
    public override void MoveToPosition(float xPosition)
    {
        _animator.SetFloat("speed", MoveSpeed);

        if (xPosition + 0.2f < gameObject.transform.position.x)
            _rigidbody2D.velocity = new Vector2(-MoveSpeed, _rigidbody2D.velocity.y);
        if (xPosition + 0.2f > gameObject.transform.position.x)
            _rigidbody2D.velocity = new Vector2(MoveSpeed, _rigidbody2D.velocity.y);

        _spriteRenderer.flipX = _rigidbody2D.velocity.x < 0.0f;
    }
    public void TakeDamage(float damage)
    {
        _animator.SetTrigger("takeHit");
        Health -= damage;
    }

    public override void Kill()
    {
        _animator.SetTrigger("death");
        Destroy(_rigidbody2D);
        Destroy(_capsuleCollider2D);
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(_attackTracking);
        Destroy(gameObject, 8);
    }

    public override void StartStand()
    {
        MoveSpeed = 0;

        if(_animator != null)
            _animator.SetTrigger("idle");
    }
    public override void StopStand()
    {
        MoveSpeed = _moveSpeed;
        _animator.SetFloat("speed", MoveSpeed);
    }

    private void EnemyPotrol()
    {
        _animator.SetFloat("speed", MoveSpeed);
        if (_isPotrolRight)
        {
            _nowPositionPotrol -= MoveSpeed;

            _rigidbody2D.velocity = new Vector2(MoveSpeed, _rigidbody2D.velocity.y);
            _spriteRenderer.flipX = _rigidbody2D.velocity.x < 0.0f;

            if (_nowPositionPotrol <= 0)
                _isPotrolRight = false;
        }
        else
        {
            _nowPositionPotrol += MoveSpeed;

            _rigidbody2D.velocity = _rigidbody2D.velocity = new Vector2(-MoveSpeed, _rigidbody2D.velocity.y);
            _spriteRenderer.flipX = _rigidbody2D.velocity.x < 0.0f;

            if (_nowPositionPotrol >= RangePotrol)
                _isPotrolRight = true;
        }
    }

    private void MoveToObject(GameObject obj, int distance)
    {
        if (Vector2.Distance(obj.transform.position, gameObject.transform.position) < distance)
        {
            _isSeePlayer = true;
            _animator.SetFloat("speed", MoveSpeed);

            if (obj.transform.position.x < gameObject.transform.position.x)
                _rigidbody2D.velocity = new Vector2(-MoveSpeed, _rigidbody2D.velocity.y);
            if (obj.transform.position.x > gameObject.transform.position.x)
                _rigidbody2D.velocity = new Vector2(MoveSpeed, _rigidbody2D.velocity.y);

            _spriteRenderer.flipX = _rigidbody2D.velocity.x < 0.0f;
        }
    }


    private void Attack(GameObject obj)
    {
        if (_timer >= 1 / AttackSpeed)
        {
            if (Vector2.Distance(obj.transform.position, gameObject.transform.position) < 0.5)
            {
                _attackTracking.Attack();
                _animator.SetTrigger("attack1");
                _timer = 0;
            }
        }
    }
}