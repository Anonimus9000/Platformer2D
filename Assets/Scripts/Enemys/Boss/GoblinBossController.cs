using UnityEngine;

public class GoblinBossController : AbstructBoss
{
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
    private ComboAttacks _comboAttacks = ComboAttacks.Attack1;

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
        if (!_player.IsDead())
            MoveToObject(_player.gameObject, 2);
    }

    public override void LookRight()
    {
        _spriteRenderer.flipX = false;
    }

    public override void LookLeft()
    {
        _spriteRenderer.flipX = true;
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

    private void Attack(GameObject objectToAttack)
    {
        if (_timer >= 1 / AttackSpeed)
        {
            if (Vector2.Distance(objectToAttack.transform.position, gameObject.transform.position) < 0.5 &&
                _comboAttacks == ComboAttacks.Attack1)
            {
                _attackTracking.Attack();
                _animator.SetTrigger("attack1");
                _timer = 0;
                CorrectQueueComboAttacks();
                return;
            }
            else if (_comboAttacks == ComboAttacks.LastAttack)
            {
                _attackTracking.Attack();
                _animator.SetTrigger("attack2");
                _timer = 0;
                CorrectQueueComboAttacks();
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
}