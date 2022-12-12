using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DetectDirection : MonoBehaviour
{
    public GameObject player;
    public Animator animator;
    
    void Update()
    {
        Direction();
    }

    void Direction()
    {
        Vector2 distance = player.transform.position - this.transform.position;
        if (distance.x > 0)
        {
            animator.SetBool("Direction", true);
        }
        if (distance.x < 0)
        {
            animator.SetBool("Direction", false);
        }
    }
}
