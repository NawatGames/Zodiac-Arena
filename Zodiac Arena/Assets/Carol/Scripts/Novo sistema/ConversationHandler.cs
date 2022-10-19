using UnityEngine;
using UnityEngine.Events;

namespace Carol.Scripts.Novo_sistema
{
    public class ConversationHandler : MonoBehaviour
    {
        public Conversation conversation;
        public int currentDialogue;
        public DialogueHandler dialogueHandler;
        public UnityEvent conversationEndedEvent = new UnityEvent();
        
        
        [ContextMenu("Start Conversation")]
        public void StartConversation()
        {
            currentDialogue = 0;

            var dialogue = conversation.dialogues[currentDialogue];
            dialogueHandler.SetDialogue(dialogue);
        }
        [ContextMenu("Next Conversation")]
        public void NextConversation()
        {
            currentDialogue++;
            if (conversation.dialogues.Count > currentDialogue)
            {
                var dialogue = conversation.dialogues[currentDialogue];
                dialogueHandler.SetDialogue(dialogue);
            }
            else
            {
                conversationEndedEvent.Invoke();
                print("Acabou!");
            }
            
        }
    }
}