using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

using Assets.Scripts.Interfaces;
public class GoblinController : MonoBehaviour, IEnemy
{
    public float Damage;
    public float Health;
    public float AttackSpeed = 2;
    public float MoveSpeed = 10f;
    public float RangePotrol = 100f;
    public bool Potrol = true;

    private AttackTrackingGoblin _attackTracking;
    private float _timer = 0;
    private float _nowPositionPotrol;
    private bool _isPotrolRight = true;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private PlayerController _player;
    

    void Start()
    {
        _attackTracking = GetComponentInChildren<AttackTrackingGoblin>();
        _nowPositionPotrol = RangePotrol;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        Attack(_player.gameObject);
    }

    void FixedUpdate()
    {
        if(Potrol)
            EnemyPotrol();
        MoveToObject(_player.gameObject, 4);
    }

    public void TakeDamage(float damage)
    {
        print("Goblin take hit");
        _animator.SetTrigger("takeHit");
        Health -= damage;
        print("goblin take back");
        if (_spriteRenderer.flipX)
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y);
        else
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - 0.3f, gameObject.transform.position.y);
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
