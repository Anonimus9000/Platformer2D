using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrackingGoblin : MonoBehaviour
{
    private GoblinController _goblin;
    private PlayerController _player;
    private SpriteRenderer _spriteRendererGoblin;
    private CircleCollider2D _circleCollider;
    void Start()
    {
        _goblin = GetComponentInParent<GoblinController>();
        _spriteRendererGoblin = GetComponentInParent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spriteRendererGoblin.flipX == true)
        {
            _circleCollider.offset = new Vector2(-0.44f, 0);
        }
        else if (_spriteRendererGoblin.flipX == false)
        {
            _circleCollider.offset = new Vector2(0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
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
        if (_player != null)
            _player.TakeDamage(_goblin.Damage);
    }
}
