using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrackingPlayer : MonoBehaviour
{
    private PlayerController _player;

    private GoblinController _goblin;
    void Start()
    {
        _player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D trigger)
    {
        if (trigger.gameObject.name == "Goblin")
        {
            _goblin = trigger.GetComponent<GoblinController>();
        }
        else
            _goblin = null;

    }

    public void Attack()
    {
        if(_goblin != null)
            _goblin.TakeDamage(_player.Damage);
    }
}
