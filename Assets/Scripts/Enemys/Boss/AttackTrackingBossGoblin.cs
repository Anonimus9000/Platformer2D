using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrackingBossGoblin : MonoBehaviour
{
    public AbstructBoss Boss;

    private PlayerController _player;
    private SpriteRenderer _spriteRendererGoblin;
    private CircleCollider2D _circleCollider;

    private void Start()
    {
        _spriteRendererGoblin = GetComponentInParent<SpriteRenderer>();
        _circleCollider = GetComponentInChildren<CircleCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (_spriteRendererGoblin.flipX == true)
            _circleCollider.offset = new Vector2(-(_circleCollider.radius * 2), 0);
        else if (_spriteRendererGoblin.flipX == false) _circleCollider.offset = new Vector2(0, 0);
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.name == "Player")
            _player = trigger.GetComponent<PlayerController>();
        else
            _player = null;
    }

    public void Attack()
    {
        if (_player != null)
            _player.TakeDamage(Boss.Damage);
    }
}