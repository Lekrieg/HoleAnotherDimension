using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public SignalMessage signalMessage;
    public UnityEvent signalMessageEvent;

    public void OnSignalMessageRaised()
    {
        signalMessageEvent.Invoke();
    }

    private void OnEnable()
    {
        signalMessage.RegisterListener(this);
    }
    private void OnDisable()
    {
        signalMessage.DeRegisterListener(this);
    }
}
