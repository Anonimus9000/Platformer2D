using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float Period;
    public GameObject ObjectToSpawn;

    private float TimeUntilNextSpawn;

    void Start()
    {
        TimeUntilNextSpawn = Period;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Spawn();
    }

    private void Spawn()
    {
        TimeUntilNextSpawn -= Time.deltaTime;

        if (TimeUntilNextSpawn <= 0.0f)
        {
            TimeUntilNextSpawn = Period;
            Instantiate(ObjectToSpawn, transform.position, transform.rotation);
        }
    }
}
