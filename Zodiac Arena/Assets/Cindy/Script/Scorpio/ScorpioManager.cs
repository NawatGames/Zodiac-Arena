using System;
using System.Collections;
using UnityEngine;

public class ScorpioManager : MonoBehaviour
{
    [Header("Atributos do Veneno Lançado pela Scorpio")]
    public Transform poisonPoint;
    public ScorpioPoison scorpioPoison;
    public float timeinseconds;
    private bool isCoroutineExecuting;

    private Ray ray;

    void Update()
    {
        StartCoroutine(ExecuteAfterTime(timeinseconds));

    }
    
    IEnumerator ExecuteAfterTime(float time)
    {
        if (isCoroutineExecuting)
            yield break;
        
        isCoroutineExecuting = true;
        
        yield return new WaitForSeconds(time);
        scorpioPoison.Shoot(poisonPoint,scorpioPoison.gameObject);

        isCoroutineExecuting = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            Destroy(other.gameObject);
        }
    }
    
}
