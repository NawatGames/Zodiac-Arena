using System.Collections.Generic;
using UnityEngine;

namespace Carol.Scripts.Novo_sistema
{
    [CreateAssetMenu(menuName = "New Conversation", fileName = "Conversation")]
    public class Conversation : ScriptableObject
    {
        public List<Dialogue> dialogues = new List<Dialogue>();
    }
}