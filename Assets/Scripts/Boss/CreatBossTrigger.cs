using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatBossTrigger : MonoBehaviour
{
    public AbstructBoss Boss;

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.name == "Player")
        {
            Instantiate(Boss);
            Destroy(gameObject);
        }
    }
}
