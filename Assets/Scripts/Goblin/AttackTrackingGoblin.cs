using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrackingGoblin : MonoBehaviour
{
    private GoblinController _goblin;

    private PlayerController _player;
    void Start()
    {
        _goblin = GetComponentInParent<GoblinController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.name == "Player")
        {
            _player = trigger.GetComponent<PlayerController>();
        }
    }
    public void Attack()
    {
        if (_player != null)
            _player.TakeDamage(_goblin.Damage);
    }
}
