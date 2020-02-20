using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tombstone : MonoBehaviour
{
    public SignalMessage signalContext;
    public bool isInRange;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isInRange && !DialogueSystem.Instance.isInteracting)
        {
            GetComponent<InteractableObjects>().Interact();
        }
        else if(Input.GetKeyDown(KeyCode.E) && DialogueSystem.Instance.isInteracting)
        {
            DialogueSystem.Instance.ContinueDialogue();
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
