using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueInput : MonoBehaviour
{
    public UnityEvent KeyPressedEvent;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            KeyPressedEvent.Invoke();
        }
    }
}
