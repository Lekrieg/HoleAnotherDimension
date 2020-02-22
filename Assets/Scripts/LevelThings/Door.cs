using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private int door;

    public SignalMessage signalContext;
    public bool isInRange;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && isInRange && !DialogueSystem.Instance.isInteracting)
        {
            GameManager.instance.GoToDoor(door);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            signalContext.Raise();
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            signalContext.Raise();
            isInRange = false;
        }
    }
}
