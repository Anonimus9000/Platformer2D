using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public abstract class AbstructBoss : MonoBehaviour, IEnemy
{
    public float RangeVision;
    public float Health;
    public float Damage;
    public float AttackSpeed;
    public float MoveSpeed;

    public abstract bool IsSeePlayer();
    public abstract void StartStand();
    public abstract void StopStand();
    public abstract void LookLeft();
    public abstract void LookRight();
    public abstract void StartFight();
    public abstract void StopFight();

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public virtual void Kill()
    {
        Destroy(gameObject);
    }
}