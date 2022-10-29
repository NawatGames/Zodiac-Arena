using System;
using System.Collections;
using System.Collections.Generic;
using Packages.Rider.Editor.UnitTesting;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float timeinseconds;
    private bool isCoroutineExecuting;
    Animator animator;
    
    private void Start()
    {
        
        animator = this.gameObject.GetComponent<Animator>();
        ColliderDisable();
        animator.speed = timeinseconds;
        animator.SetFloat("time", Convert.ToSingle(0.5));
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
