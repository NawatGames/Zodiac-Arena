using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerr : MonoBehaviour
{
    private int i = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Danger"))
        {
            Debug.Log("Hit " + ++i);
            //UnityEditor.EditorApplication.isPlaying = false; //apenas no editor
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Danger"))
        {
            Debug.Log("Hit " + ++i);
            //UnityEditor.EditorApplication.isPlaying = false; //apenas no editor
        }
    }
}
