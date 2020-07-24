using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    public Text PlayerText;
    public Text AnyCharText;
    public bool PlayerTalkFirst;

    private bool _nowPlayerTalk;
    private Queue<string> sensencesPlayer;
    private Queue<string> sensencesEnemy;
    void Start()
    {
        _nowPlayerTalk = PlayerTalkFirst;
        sensencesPlayer = new Queue<string>();
        sensencesEnemy = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit"))
            DisplayNextSentence();
    }

    public void StartDialog(Dialog dialog)
    {
        sensencesPlayer.Clear();

        foreach (string sensence in dialog.sentencesPlayer)
        {
            sensencesPlayer.Enqueue(sensence);
        }

        foreach (string sensence in dialog.sentencesEnemy)
        {
            sensencesEnemy.Enqueue(sensence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (sensencesPlayer.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence;

        if (_nowPlayerTalk)
            sentence = sensencesPlayer.Dequeue();
        
        else
            sentence = sensencesEnemy.Dequeue();
        
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        if (_nowPlayerTalk)
        {
            PlayerText.text = "";
            _nowPlayerTalk = !_nowPlayerTalk;

            foreach (var letter in sentence.ToCharArray())
            {
                if (_nowPlayerTalk)
                    PlayerText.text += letter;
                else
                    AnyCharText.text += letter;
                yield return null;
            }
        }

        else if (!_nowPlayerTalk)
        {
            AnyCharText.text = "";
            _nowPlayerTalk = !_nowPlayerTalk;

            foreach (var letter in sentence.ToCharArray())
            {
                if (_nowPlayerTalk)
                    PlayerText.text += letter;
                else
                    AnyCharText.text += letter;
                yield return null;
            }
        }

    }

    private void EndDialog()
    {
        Debug.Log("End dialog");
    }

}
