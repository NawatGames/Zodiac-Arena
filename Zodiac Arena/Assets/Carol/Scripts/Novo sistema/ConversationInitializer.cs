using System.Collections;
using System.Collections.Generic;
using Carol.Scripts.Novo_sistema;
using UnityEngine;

public class ConversationInitializer : MonoBehaviour
{
    [SerializeField] private ConversationHandler conversationHandler;
    void Start()
    {
        conversationHandler.StartConversation();
    }

   
        
    
}
