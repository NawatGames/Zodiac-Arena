using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool isTimeCounting = true;
    public float timeRemaining = 30f;
    public Text timerTxt;
    
    void Update()
    {
        if(!isTimeCounting) return;
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        else
        {
            isTimeCounting = false;
            Debug.Log("Tempo esgotado!!");
        }
        DisplayTime(timeRemaining);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0) timeToDisplay = 0;
        timerTxt.text = string.Format("{0:00}", timeToDisplay);
    }
}
