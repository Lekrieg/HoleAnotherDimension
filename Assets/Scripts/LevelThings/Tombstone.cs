using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tombstone : MonoBehaviour
{
    public SignalMessage signalContext;
    public bool isInRange;

    public GameObject dialoguePanel;
    public  Text dialogueText;
    public Text nameText;
    int dialogueIndex;

    [SerializeField]
    string nome;
    [SerializeField]
    [TextArea]
    string[] frases;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange && GameManager.instance.isInteracting == false)
        {
            if(dialoguePanel.activeInHierarchy)
            {
                dialoguePanel.SetActive(false);
            }
            else
            {
                GameManager.instance.isInteracting = true;
                dialoguePanel.SetActive(true);
                dialogueText.text = frases[dialogueIndex];
                nameText.text = nome;
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && isInRange && GameManager.instance.isInteracting == true)
        {
            if(dialogueIndex < frases.Length - 1)
            {
                dialogueIndex++;
                dialogueText.text = frases[dialogueIndex];
            }
            else
            {
                dialogueIndex = 0;
                GameManager.instance.isInteracting = false;
                dialoguePanel.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            signalContext.Raise();
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            signalContext.Raise();
            isInRange = false;
        }
    }
}
