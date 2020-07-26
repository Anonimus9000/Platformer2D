using UnityEngine;

public class DialogBossTrigger : MonoBehaviour
{
    public bool PlayerTalkFirst = true;
    public AbstructBoss ObjectToTalk;
    public Dialog Dialog;

    private BoxCollider2D _boxCollider;
    private PlayerController _player;
    private DialogManager _dialogManager;
    void Start()
    {
        _dialogManager = FindObjectOfType<DialogManager>();
        ObjectToTalk.StartStand();
    }

    // Update is called once per frame
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
