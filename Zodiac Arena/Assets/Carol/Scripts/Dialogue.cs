using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //To show in the inspector and edit
public class Dialogue
{
    public string name;
    
    [TextArea(3, 50)] //Specify the number of lines (min to max) of a sentence
    public string[] sentences;


}
