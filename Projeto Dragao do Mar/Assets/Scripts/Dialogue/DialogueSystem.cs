using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

public class DialogueSystem : MonoBehaviour
{
    private Queue<string> sentences;

    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image characterImage;
    public GameObject dialogueBox;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Character charac)
    {
        Debug.Log("Starting dialogue with " + charac.nome);
        nameText.text = charac.nome;
        characterImage.sprite = charac.charcterImage;
        dialogueBox.SetActive(true);

        sentences.Clear();

        foreach (string sentence in charac.sentences) 
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0) 
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        Debug.Log("End up Conversation");
        dialogueBox.SetActive(false);
    }
}
//https://www.youtube.com/watch?v=_nRzoTzeyxU
//https://jogoscomcafe.wordpress.com/2021/04/08/tutorial-sistema-de-dialogo-estilo-jrpg-unity/