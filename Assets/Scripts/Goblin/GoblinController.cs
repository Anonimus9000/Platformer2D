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
    private float _nowPositionPotrol;
    private bool _isPotrolRight = true;
    private bool _isDead = false;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private PlayerController _player;
    private CapsuleCollider2D _capsuleCollider2D;


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

    // Update is called once per frame
    void Update()
    {
        if (_isDead)
            Kill();
        else if (_isSeePlayer)
            Attack(_player.gameObject);

        _timer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (Potrol)
            EnemyPotrol();
        if (!_player.IsDead())
        {
        }

        MoveToObject(_player.gameObject, 4);
    }

    public override void MoveToPosition(Vector3 position)
    {
        _animator.SetFloat("speed", MoveSpeed);
        if (position.x + 0.1f < gameObject.transform.position.x)
            _rigidbody2D.velocity = new Vector2(-MoveSpeed, _rigidbody2D.velocity.y);
        if (position.x + 0.1f > gameObject.transform.position.x)
            _rigidbody2D.velocity = new Vector2(MoveSpeed, _rigidbody2D.velocity.y);

        _spriteRenderer.flipX = _rigidbody2D.velocity.x < 0.0f;
    }
    public void TakeDamage(float damage)
    {
        _animator.SetTrigger("takeHit");
        Health -= damage;
        if (Health <= 0)
            _isDead = true;
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