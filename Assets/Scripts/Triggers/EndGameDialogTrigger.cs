using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameDialogTrigger : MonoBehaviour
{
    private PlayerController _player;
    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.tag == "Player")
        {
            _player = trigger.GetComponent<PlayerController>();
            _player.Kill();
        }
    }
}
