using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public bool PlayerTalkFirst = true;
    public EnemyMob ObjectToTalk;
    public Dialog Dialog;

    private bool _dialogIsStart = false;
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
        if (_player != null && ObjectToTalk != null)
        {
            if (_player.IsLookRight())
                ObjectToTalk.LookLeft();
            else if (_player.IsLookLeft())
                ObjectToTalk.LookRight();
        }

        if (_player != null)
        {
            if (_dialogManager.DialogIsEnd() == true && !_player.IsFight())
            {
                _player.StopStand();

                if (ObjectToTalk != null)
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
            bool temp = false;
            _dialogManager.SetDialogIsEnd(temp);

            _player = trigger.GetComponent<PlayerController>();
            if (!_player.IsFight())
            {
                _player.StartStand();

                TriggerDialog();
            }
        }
        else
        {
            _player = null;
        }
    }
}