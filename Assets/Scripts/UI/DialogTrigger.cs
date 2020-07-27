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
        ObjectToTalk.StartStand();
    }

    void FixedUpdate()
    {
        if (_player != null)
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