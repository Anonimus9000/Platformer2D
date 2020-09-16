using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrackingEnemy : MonoBehaviour
{
    public EnemyMob Enemy;

    private PlayerController _player;
    private SpriteRenderer _spriteRendererGoblin;
    private CircleCollider2D _circleCollider;

    void Start()
    {
        _spriteRendererGoblin = GetComponentInParent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        if (_spriteRendererGoblin.flipX == true)
        {
            _circleCollider.offset = new Vector2(-(_circleCollider.radius * 2), 0);
        }
        else if (_spriteRendererGoblin.flipX == false)
        {
            _circleCollider.offset = new Vector2(0, 0);
        }
    }

    void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.name == "Player")
        {
            _player = trigger.GetComponent<PlayerController>();
        }
        else
        {
            _player = null;
        }
    }

    public void Attack()
    {
        if (_player != null && Enemy != null)
            _player.TakeDamage(Enemy.Damage);
    }
}