using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
	public static DialogueSystem Instance;

	private GameObject dialoguePanel;

	public List<string> dialogueLines = new List<string>();
	public string npcName;

	Text dialogueText;
	Text nameText;
	int dialogueIndex;

	public bool isInteracting = false;

	void Awake()
	{
		Instance = this;

		dialoguePanel = GameObject.Find("DialoguePanel");
		dialogueText = GameObject.Find("DialogueText").GetComponent<Text>();
		nameText = GameObject.Find("NomeText").GetComponent<Text>();

		dialoguePanel.SetActive(false);
	}

	public void AddDialogue(string[] lines, string npcName)
	{
		dialogueIndex = 0;

		dialogueLines = new List<string>(lines.Length);
		dialogueLines.AddRange(lines);

		this.npcName = npcName;

		CreateDialogue();
	}

	public void CreateDialogue()
	{
		dialogueText.text = dialogueLines[dialogueIndex];
		nameText.text = npcName;
		dialoguePanel.SetActive(true);
	}

	// Colocar para ativar quando apertar uma tecla
	public void ContinueDialogue()
	{
		if (dialogueIndex < dialogueLines.Count - 1)
		{
			dialogueIndex++;
			dialogueText.text = dialogueLines[dialogueIndex];
		}
		else
		{
			isInteracting = false;
			dialoguePanel.SetActive(false);
		}
	}
}
