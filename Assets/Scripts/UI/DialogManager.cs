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
    private Text _dialogText;
    private Queue<string> sensences;
    void Start()
    {
        _nowPlayerTalk = PlayerTalkFirst;
        sensences = new Queue<string>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit"))
            DisplayNextSentence();
    }

    public void StartDialog(Dialog dialog)
    {
        sensences.Clear();

        foreach (string sensence in dialog.sentencesPlayer)
        {
            sensences.Enqueue(sensence);
        }

        DisplayNextSentence();
    }

    private void DisplayNextSentence()
    {
        if (sensences.Count == 0)
        {
            EndDialog();
            return;
        }

        string sentence = sensences.Dequeue();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        _dialogText.text += "";
        foreach (var letter in sentence.ToCharArray())
        {
            if (_nowPlayerTalk)
                PlayerText.text += letter;
            else
                AnyCharText.text += letter;
            yield return null;
        }
    }

    private void EndDialog()
    {
        Debug.Log("End dialog");
    }

}
