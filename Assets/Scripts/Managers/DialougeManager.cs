using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public static DialogueManager Instance;
    public PlayerScript player;
    [SerializeField] private GameObject dialogueUI;

    private Queue<string> sentences;
    public bool isDialogueActive = false;

    void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        sentences = new Queue<string>();
    }

    void Update()
    {
        if (isDialogueActive && Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence();
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        dialogueUI.SetActive(true);
        nameText.text = dialogue.name;
        isDialogueActive = true;
        player.canMove = false;
        player.canAttack = false;
        player.canJump = false;

        sentences.Clear();
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text="";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text+=letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        dialogueUI.SetActive(false);
        isDialogueActive = false;
        player.canMove = true;
        player.canAttack = true;
        player.canJump = true;
    }
}
