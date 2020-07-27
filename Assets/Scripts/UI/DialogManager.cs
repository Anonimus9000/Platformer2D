using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text PlayerText;
    public Text AnyCharText;
    public Text TabToNextText;

    private bool _isEndPrintText;
    private bool _nowPlayerTalk;
    private bool _dialogIsEnd = true;
    private Queue<string> sensencesPlayer;
    private Queue<string> sensencesEnemy;

    void Start()
    {
        sensencesPlayer = new Queue<string>();
        sensencesEnemy = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit") && _isEndPrintText)
            DisplayNextSentence();
    }

    void FixedUpdate()
    {
    }

    public bool DialogIsEnd()
    {
        return _dialogIsEnd;
    }

    public void StartDialog(Dialog dialog, bool PlayerTalkFirst)
    {
        _nowPlayerTalk = PlayerTalkFirst;

        TabToNextText.text = "Tab enter to next...";

        _dialogIsEnd = false;

        sensencesPlayer.Clear();
        sensencesEnemy.Clear();

        foreach (string sensence in dialog.SentencesPlayer)
        {
            sensencesPlayer.Enqueue(sensence);
        }

        foreach (string sensence in dialog.SentencesEnemy)
        {
            sensencesEnemy.Enqueue(sensence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (sensencesPlayer.Count == 0 && sensencesEnemy.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = "";

        if (_nowPlayerTalk)
            sentence = sensencesPlayer.Dequeue();

        else
            sentence = sensencesEnemy.Dequeue();

        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        _isEndPrintText = false;
        if (_nowPlayerTalk)
        {
            PlayerText.text = "";

            foreach (var letter in sentence.ToCharArray())
            {
                PlayerText.text += letter;
                yield return null;
            }

            _nowPlayerTalk = !_nowPlayerTalk;
        }

        else if (!_nowPlayerTalk)
        {
            AnyCharText.text = "";

            foreach (var letter in sentence.ToCharArray())
            {
                AnyCharText.text += letter;
                yield return null;
            }

            _nowPlayerTalk = !_nowPlayerTalk;
        }

        _isEndPrintText = true;
    }

    private void EndDialog()
    {
        print("Dialog is end");
        _dialogIsEnd = true;
        PlayerText.text = "";
        AnyCharText.text = "";
        TabToNextText.text = "";
    }
}