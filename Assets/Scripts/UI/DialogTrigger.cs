using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public EnemyMob ObjectToTalk;
    public Dialog Dialog;
    
    private BoxCollider2D _boxCollider;
    private PlayerController _player;
    private void TriggerDialog()
    {
        FindObjectOfType<DialogManager>().StartDialog(Dialog);
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.name == "Player")
        {
            _player = trigger.GetComponent<PlayerController>();
            _player.Stand();

            ObjectToTalk.MoveToPosition(_player.transform.position);
            ObjectToTalk.Stand();

            TriggerDialog();

            _player.NoStay();
            ObjectToTalk.NoStay();

        }
        else
        {
            _player = null;
        }
    }

}
