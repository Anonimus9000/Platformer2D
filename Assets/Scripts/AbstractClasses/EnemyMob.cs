using Assets.Scripts.Interfaces;
using UnityEngine;

public abstract class EnemyMob : MonoBehaviour, IEnemy
{
    public float Damage;
    public float Health;
    public float AttackSpeed = 2;
    public float MoveSpeed = 10f;
    public float RangePotrol = 100f;
    public bool Potrol = true;

    private float _moveSpeed;
    void Start()
    {
        _moveSpeed = MoveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public abstract void Kill();

    public abstract void MoveToPosition(Vector3 position);
    public virtual void Attack()
    {

    }

    public virtual void TakeDamage(float damage)
    {
        Health -= damage;
    }

    public void Stand()
    {
        MoveSpeed = 0;
    }

    public void NoStay()
    {
        MoveSpeed = _moveSpeed;
    }


}
