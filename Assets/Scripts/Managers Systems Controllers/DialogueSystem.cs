using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueSystem : MonoBehaviour
{
    public static DialogueSystem instance;

    private List<string> lines = new List<string>();
    private new string name;
    [SerializeField] private GameObject dialogueUI;
    private Button continueButton;
    private TMP_Text nameText, dialogueText;
    private int dialogueIndex;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        continueButton = dialogueUI.transform.Find("Continue")?.GetComponent<Button>();
        continueButton.onClick.AddListener(delegate { ContinueDialogue(); });
        dialogueText = dialogueUI.transform.Find("DialogueText").GetComponent<TMP_Text>();
        nameText = dialogueUI.transform.Find("Name").GetChild(0).GetComponent<TMP_Text>();
        dialogueUI.SetActive(false);
    }

    public void AddNewDialogue(string[] lines, string name)
    {
        this.lines = new List<string>(lines.Length);
        this.lines.AddRange(lines);
        this.name = name;
        dialogueIndex = 0;
        CreateDialogue();
    }

    private void CreateDialogue()
    {
        dialogueText.text = lines[dialogueIndex];
        nameText.text = name;
        dialogueUI.SetActive(true);
    }

    public void ContinueDialogue()
    {
        if (dialogueIndex < lines.Count - 1)
        {
            dialogueText.text = lines[++dialogueIndex];
        }
        else
        {
            dialogueUI.SetActive(false);
        }
    }
}
