using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public Spawner Spawner;

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.name == "Player")
        {
            Spawner.StartSpawn();
        }
    }
}