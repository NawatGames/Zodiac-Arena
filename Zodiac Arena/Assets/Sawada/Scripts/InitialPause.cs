using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPause : MonoBehaviour
{
    public float initialPauseTime = 3f;
    void Awake()
    {
        StartCoroutine(InitialPauseCall());
    }
    private IEnumerator InitialPauseCall()
    {
        // Debug.Log("inicio pause");
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(initialPauseTime);
        Time.timeScale = 1;
        // Debug.Log("fim pause");
    }

}
