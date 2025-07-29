using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum DialogueMode  
{ 
    Dialogue,
    Explanation
}

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI 연결")]
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    public GameObject explanationPanel;
    public TextMeshProUGUI explanationText;

    private Queue<string> dialogueLines = new Queue<string>();
    private bool isDialogueActive = false;
    private bool isTyping = false;
    private Coroutine typingCoroutine;

    private DialogueMode currentMode;

    // 다이얼로그 종료 시 발생하는 이벤트
    public event Action onDialogueEnd;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialoguePanel.SetActive(false);
        explanationPanel.SetActive(false);
    }

    private void Update()
    {
        if (isDialogueActive && Input.GetKeyDown(KeyCode.Space))
        {
            if (isTyping)
                SkipTyping();
            else
                DisplayNextLine();
        }
    }

    public void StartDialogue(string[] lines, DialogueMode mode = DialogueMode.Dialogue)
    {
        dialogueLines.Clear();
        foreach (string line in lines)
            dialogueLines.Enqueue(line);

        currentMode = mode;
        isDialogueActive = true;

        if (mode == DialogueMode.Dialogue)
        {
            dialoguePanel.SetActive(true);
            explanationPanel.SetActive(false);
        }
        else
        {
            explanationPanel.SetActive(true);
            dialoguePanel.SetActive(false);
        }

        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        if (dialogueLines.Count == 0)
        {
            CloseDialogue();
            return;
        }

        string nextLine = dialogueLines.Dequeue();

        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(nextLine));
    }

    private IEnumerator TypeText(string line)
    {
        isTyping = true;

        if (currentMode == DialogueMode.Dialogue)
            dialogueText.text = "";
        else
            explanationText.text = "";

        foreach (char letter in line)
        {
            if (currentMode == DialogueMode.Dialogue)
                dialogueText.text += letter;
            else
                explanationText.text += letter;

            yield return new WaitForSeconds(0.03f);
        }

        isTyping = false;
    }

    private void SkipTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        string currentLine = dialogueLines.Count > 0 ? dialogueLines.Peek() : "";

        if (currentMode == DialogueMode.Dialogue)
            dialogueText.text = currentLine;
        else
            explanationText.text = currentLine;

        isTyping = false;
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        explanationPanel.SetActive(false);
        isDialogueActive = false;
        dialogueText.text = "";
        explanationText.text = "";

        //  종료 이벤트 호출
        onDialogueEnd?.Invoke();
        onDialogueEnd = null; // 이벤트 중복 방지 (한 번만 실행되도록)
    }
}
