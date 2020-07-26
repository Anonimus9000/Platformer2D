using Assets.Scripts.Interfaces;
using UnityEngine;

public abstract class EnemyMob : MonoBehaviour, IEnemy
{
    public float Damage;
    public float Health;
    public float AttackSpeed = 2;
    public float MoveSpeed = 1f;
    public float RangeVision = 4;
    public float RangePotrol = 100f;
    public bool Potrol = true;

    private float _StartMoveSpeed;
    void Start()
    {
        _StartMoveSpeed = MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public abstract void Kill();

    public abstract void MoveToPosition(Vector3 position);

    public abstract void MoveToPosition(float xPosition);
    public abstract void LookRight();

    public abstract void LookLeft();
    public virtual void Attack()
    {

    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public virtual void StartStand()
    {
        MoveSpeed = 0;
    }

    public virtual void StopStand()
    {
        MoveSpeed = _StartMoveSpeed;
    }

    

}
