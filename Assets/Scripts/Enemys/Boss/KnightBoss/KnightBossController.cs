using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightBossController : AbstructBoss
{
    private bool _isFight = false;
    private float _moveSpeed;
    private bool _isSeePlayer = false;
    private float _timer;
    private bool _isDead = false;
    private AttackTrackingBossGoblin _attackTracking;
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
        _attackTracking = GetComponentInChildren<AttackTrackingBossGoblin>();
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
        else if (!_player.IsDead())
            Attack(_player.gameObject);

        _timer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (Health > 0)
        {
            if (!_player.IsDead())
                MoveToObject(_player.gameObject);

            if (IsSeePlayer())
                StartFight();
            else if (_isFight)
            {
                StopFight();
                _isFight = false;
            }
        }
        else if (_isFight)
        {
            StopFight();
            _isFight = false;
        }
    }

    public override void LookRight()
    {
        _spriteRenderer.flipX = false;
    }

    public override void LookLeft()
    {
        _spriteRenderer.flipX = true;
    }

    public override void StartFight()
    {
        _player.StartFight();
    }

    public override void StopFight()
    {
        _player.StopFight();
    }

    public override void StartStand()
    {
        MoveSpeed = 0;
    }

    public override void StopStand()
    {
        MoveSpeed = _moveSpeed;
    }

    public override void TakeDamage(float damage)
    {
        _animator.SetTrigger("takeHit");
        Health -= damage;
        if (Health <= 0)
            _isDead = true;
    }

    public override void Kill()
    {
        _isSeePlayer = false;
        _animator.SetTrigger("death");
        Destroy(_rigidbody2D);
        Destroy(_capsuleCollider2D);
        Destroy(GetComponent<BoxCollider2D>());
        Destroy(_attackTracking);
        Destroy(gameObject, 8);
    }

    public override bool IsSeePlayer()
    {
        return _isSeePlayer;
    }

    private void MoveToObject(GameObject obj)
    {
        if (Vector2.Distance(obj.transform.position, gameObject.transform.position) < RangeVision)
        {
            _isSeePlayer = true;
            _isFight = true;
            _player.StartFight();

            _animator.SetFloat("speed", Mathf.Abs(_rigidbody2D.velocity.x));

            if (obj.transform.position.x < gameObject.transform.position.x)
                _rigidbody2D.velocity = new Vector2(-MoveSpeed, _rigidbody2D.velocity.y);
            if (obj.transform.position.x > gameObject.transform.position.x)
                _rigidbody2D.velocity = new Vector2(MoveSpeed, _rigidbody2D.velocity.y);

            _spriteRenderer.flipX = _rigidbody2D.velocity.x < 0.0f;
        }
        else if (obj.tag == "Player")
            _isSeePlayer = false;
    }

    private void Attack(GameObject ObjectToAttack)
    {
        if (_timer >= 1 / AttackSpeed)
        {
            if (ObjectToAttack != null)
            {
                if (Vector2.Distance(ObjectToAttack.transform.position, gameObject.transform.position) < 0.5)
                {
                    _attackTracking.Attack();
                    _animator.SetTrigger("attack1");
                    _timer = 0;
                }
            }
        }
    }
}