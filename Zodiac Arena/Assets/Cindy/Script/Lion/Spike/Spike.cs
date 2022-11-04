using System;
using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spike : MonoBehaviour
{
    public float timeinseconds;
    private bool isCoroutineExecuting;

    private void Awake()
    {
        ColliderDisable();
    }
    
    void ColliderEnable()
    {
        
        
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        StartCoroutine(ExecuteAfterTime(timeinseconds));
      
    }
    
    void ColliderDisable()
    {
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
      
    }
    
    // Escolher o tempo que quer deixar o espinho on antes de sair de cena
    IEnumerator ExecuteAfterTime(float time)
    {
        if (isCoroutineExecuting)
            yield break;
        
        isCoroutineExecuting = true;
        
        yield return new WaitForSeconds(Random.Range(1,timeinseconds));
        Destroy(this.gameObject);

        isCoroutineExecuting = false;
    }

    // Quando o Player entrar nessa área, ele morre
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
