using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : MonoBehaviour
{
    public float timeinseconds;
    private bool isCoroutineExecuting;
    
    private void Start()
    {
        ColliderDisable();
    }
    
    void ColliderEnable()
    {
        
        this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
      
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
        
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);

        isCoroutineExecuting = false;
    }
    void Update()
    {
        StartCoroutine(ExecuteAfterTime(timeinseconds));

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
