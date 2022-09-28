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
        transform.Translate(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * 5*Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("CapricornBeam"))
        {
            Debug.Log("Hit " + i++);
        }
    }
}
