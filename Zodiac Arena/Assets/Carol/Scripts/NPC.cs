using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Dialogue trigger
public class NPC : MonoBehaviour
{
    public Carol.Scripts.Novo_sistema.Dialogue dialogue;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
