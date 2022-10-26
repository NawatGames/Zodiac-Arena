using UnityEngine;
using UnityEngine.UI;

namespace Carol.Scripts.Novo_sistema
{
    public class DialogueHandler : MonoBehaviour
    {
        public Image image;
        public Text text;
        public int currentSentence;
        public Dialogue currentDialogue;
        public ConversationHandler conversationHandler;

        [ContextMenu("Next Sentence")]
        public void NextSentence()
        {
            currentSentence++;
            if(currentDialogue.sentences.Count > currentSentence)
            {
                Refresh();
            }
            else
            {
                conversationHandler.NextConversation();
            }
            
        }

        private void Refresh()
        {
             image.sprite = currentDialogue.sprite;
             text.text = currentDialogue.sentences[currentSentence];
        }

        public void SetDialogue(Dialogue dialogue)
        {
            currentDialogue = dialogue;
            currentSentence = 0;
            Refresh();
        }
    }
}