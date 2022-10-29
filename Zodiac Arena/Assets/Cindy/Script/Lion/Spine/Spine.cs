using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spine : MonoBehaviour
{
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
    
    // Quando o Player entrar nessa área, ele morre
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
