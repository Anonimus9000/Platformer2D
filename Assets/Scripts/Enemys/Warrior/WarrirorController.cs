using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Weapon
{
    Sword,
    Spear,
    SawSword
}

public class WarrirorController : EnemyMob
{
    public Weapon Weapon;

    private bool _isDead = false;
    private bool _isSeePlayer = false;
    private bool _isFight = false;
    private AttackTrackingEnemy _attackTracking;
    private float _timer = 0;
    private float _moveSpeed;
    private float _nowPositionPotrol;
    private bool _isPotrolRight = false;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private PlayerController _player;
    private CapsuleCollider2D _capsuleCollider2D;
    private ComboAttacks _comboAttacks = ComboAttacks.Attack1;
    private EnemyAudioController _enemyAudioController;

    private enum ComboAttacks
    {
        Attack1,
        LastAttack
    }

    void Awake()
    {
        _moveSpeed = MoveSpeed;
    }

    void Start()
    {
        _attackTracking = GetComponentInChildren<AttackTrackingEnemy>();
        _nowPositionPotrol = RangePotrol;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerController>();
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _enemyAudioController = GetComponentInChildren<EnemyAudioController>();
    }


    void Update()
    {
        if (Health <= 0 && !_isDead)
        {
            _isDead = true;
            Kill();
        }

        if (_isSeePlayer && !_isDead)
            Attack(_player.gameObject);

        _timer += Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (Health > 0)
        {
            if (Potrol && !IsSeePlayer())
                EnemyPotrol();

            if (MoveSpeed > 0)
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


    public override void LookLeft()
    {
        _spriteRenderer.flipX = true;
    }

    public override void LookRight()
    {
        _spriteRenderer.flipX = false;
    }

    public override void TakeDamage(float damage)
    {
        _animator.SetTrigger("takeHit");
        _enemyAudioController.PlayTakeDamageAudio();
        Health -= damage;
    }

    public override void StartStand()
    {
        MoveSpeed = 0;

        if (_animator != null)
            _animator.SetTrigger("idle");
    }

    public override void StopStand()
    {
        MoveSpeed = _moveSpeed;
        _animator.SetFloat("speed", MoveSpeed);
    }

    public override void StartFight()
    {
        _player.StartFight();
    }

    public override void StopFight()
    {
        _player.StopFight();
    }

    public bool Isdead()
    {
        return _isDead;
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

    public override void MoveToPosition(Vector3 position)
    {
        _animator.SetFloat("speed", Mathf.Abs(_rigidbody2D.velocity.x));

        if (position.x + 0.2f < gameObject.transform.position.x)
            _rigidbody2D.velocity = new Vector2(-MoveSpeed, _rigidbody2D.velocity.y);
        if (position.x + 0.2f > gameObject.transform.position.x)
            _rigidbody2D.velocity = new Vector2(MoveSpeed, _rigidbody2D.velocity.y);

        _spriteRenderer.flipX = _rigidbody2D.velocity.x < 0.0f;
    }

    public override void MoveToPosition(float xPosition)
    {
        _animator.SetFloat("speed", Mathf.Abs(_rigidbody2D.velocity.x));

        if (xPosition + 0.2f < gameObject.transform.position.x)
            _rigidbody2D.velocity = new Vector2(-MoveSpeed, _rigidbody2D.velocity.y);
        if (xPosition + 0.2f > gameObject.transform.position.x)
            _rigidbody2D.velocity = new Vector2(MoveSpeed, _rigidbody2D.velocity.y);

        _spriteRenderer.flipX = _rigidbody2D.velocity.x < 0.0f;
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
        else
            _isSeePlayer = false;
    }
    private bool IsSeePlayer()
    {
        return _isSeePlayer;
    }

    private void EnemyPotrol()
    {
        _animator.SetFloat("speed", Mathf.Abs(_rigidbody2D.velocity.x));
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

    private void Attack(GameObject obj)
    {
        if (Weapon == Weapon.Spear)
            AttackSpear(obj);
        else if (Weapon == Weapon.SawSword)
            AttackSawSword(obj);
        else
        {
            if (_timer >= 1 / AttackSpeed)
            {
                if (Vector2.Distance(obj.transform.position, gameObject.transform.position) < 0.5 &&
                    _comboAttacks == ComboAttacks.Attack1)
                {
                    _attackTracking.Attack();
                    _animator.SetTrigger("attack1");
                    _enemyAudioController.PlayAttackAudio();
                    _timer = 0;
                    CorrectQueueComboAttacks();
                    return;
                }
                else if (_comboAttacks == ComboAttacks.LastAttack)
                {
                    _attackTracking.Attack();
                    _animator.SetTrigger("attack2");
                    _enemyAudioController.PlayAttackAudio();
                    _timer = 0;
                    CorrectQueueComboAttacks();
                    return;
                }
            }
        }
    }

    private void AttackSpear(GameObject obj)
    {
        if (_timer >= 1 / AttackSpeed)
        {
            if (Vector2.Distance(obj.transform.position, gameObject.transform.position) < 0.5 && Weapon == Weapon.Spear)
            {
                _attackTracking.Attack();
                _animator.SetTrigger("attackSpear");
                _timer = 0;
            }
        }
    }

    private void AttackSawSword(GameObject obj)
    {
        if (_timer >= 1 / AttackSpeed)
        {
            if (Vector2.Distance(obj.transform.position, gameObject.transform.position) < 0.5 &&
                Weapon == Weapon.SawSword)
            {
                _attackTracking.Attack();
                _animator.SetTrigger("attackSawSword");
                _timer = 0;
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
}