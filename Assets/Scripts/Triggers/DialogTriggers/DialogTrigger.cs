using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public bool PlayerTalkFirst = true;
    public EnemyMob ObjectToTalk;
    public Dialog Dialog;

    private BoxCollider2D _boxCollider;
    private PlayerController _player;
    private DialogManager _dialogManager;

    void Start()
    {
        _dialogManager = FindObjectOfType<DialogManager>();
        if(ObjectToTalk != null)
            ObjectToTalk.StartStand();
    }

    void FixedUpdate()
    {
        if(_player != null)
            print("player not null");
        if (_player != null && ObjectToTalk != null)
        {
            if (_player.IsLookRight())
                ObjectToTalk.LookLeft();
            else if (_player.IsLookLeft())
                ObjectToTalk.LookRight();
        }

        if (_dialogManager.DialogIsEnd() == true)
        {
            if (_player != null)
            {
                _player.StopStand();

                if(ObjectToTalk != null)
                    ObjectToTalk.StopStand();

                Destroy(gameObject);
            }
        }
    }

    private void TriggerDialog()
    {
        _dialogManager.StartDialog(Dialog, PlayerTalkFirst);
    }

    void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.gameObject.name == "Player")
        {
            _player = trigger.GetComponent<PlayerController>();

            _player.StartStand();

            TriggerDialog();
        }
        else
        {
            _player = null;
        }
    }
}