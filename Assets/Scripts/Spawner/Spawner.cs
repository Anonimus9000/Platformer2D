using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float Period;
    public bool LimitedSpawn = false;
    public int SumSpawn;
    public GameObject ObjectToSpawn;

    private bool _isStart = false;
    private int _countSpawned;
    private float TimeUntilNextSpawn;

    void Start()
    {
        TimeUntilNextSpawn = Period;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_isStart)
        {
            if (LimitedSpawn == true)
            {
                if (_countSpawned < SumSpawn)
                    Spawn();
            }
            else
                Spawn();
        }
    }

    public bool StartSpawn()
    {
        _isStart = true;
        return _isStart;
    }

    private void Spawn()
    {
        TimeUntilNextSpawn -= Time.deltaTime;

        if (TimeUntilNextSpawn <= 0.0f)
        {
            _countSpawned += 1;
            TimeUntilNextSpawn = Period;
            Instantiate(ObjectToSpawn, transform.position, transform.rotation);
        }
    }
}
