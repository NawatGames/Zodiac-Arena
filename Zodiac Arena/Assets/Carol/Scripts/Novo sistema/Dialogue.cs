using System;
using System.Collections.Generic;
using UnityEngine;

namespace Carol.Scripts.Novo_sistema
{
    [CreateAssetMenu(menuName = "Create Dialogue", fileName = "New Dialogue")]
    public class Dialogue : ScriptableObject
    {
        public Sprite sprite;
        
       [TextArea(3, 50)]
        public List<string> sentences = new List<string>();
    }
}
