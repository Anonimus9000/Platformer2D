using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class AttackTrackingPlayer : MonoBehaviour
{
    public LayerMask _layerMask;

    private PlayerController _player;
    private IEnemy _enemy;
    private SpriteRenderer _spriteRendererPlayer;
    private CircleCollider2D _circleCollider;
    void Start()
    {
        _player = GetComponentInParent<PlayerController>();
        _spriteRendererPlayer = GetComponentInParent<SpriteRenderer>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_spriteRendererPlayer.flipX == true)
        {
            _circleCollider.offset = new Vector2(-1f, 0);
        }
        else if(_spriteRendererPlayer.flipX == false)
        {
            _circleCollider.offset = new Vector2(0, 0);
        }
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.name == "Goblin")
        {
            _enemy = trigger.GetComponent<GoblinController>();
        }
        else
        {
            _enemy = null;
        }
    }

    public void Attack()
    {
        if (_enemy != null)
            _enemy.TakeDamage(_player.Damage);
        _enemy = null;
    }

    //public void Attack()
    //{
    //    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(gameObject.transform.position, _circleCollider.radius, _layerMask);
    //    for (int i = 0; i < enemiesToDamage.Length; i++)
    //    {
    //        if(enemiesToDamage[i].GetComponent<IEnemy>() != null)
    //            enemiesToDamage[i].GetComponent<IEnemy>().TakeDamage(_player.Damage);
    //    }
    //}
}
