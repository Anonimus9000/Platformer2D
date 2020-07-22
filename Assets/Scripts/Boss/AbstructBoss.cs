using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public abstract class AbstructBoss : MonoBehaviour, IEnemy
{
    public float Health;
    public float Damage;
    public float AttackSpeed;
    public float MoveSpeed;

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public virtual void Kill()
    {
        Destroy(gameObject);
    }

    public abstract bool IsSeePlayer();

}
