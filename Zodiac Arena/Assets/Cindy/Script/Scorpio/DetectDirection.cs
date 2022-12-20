using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DetectDirection : MonoBehaviour
{
    public GameObject player;
    public Animator animator;

    public ScorpioPoison scorpioPoison;
    private ScorpioManager scorpioManager;
    private void Start()
    {
        scorpioManager = this.GetComponent<ScorpioManager>();
    }

    void Update()
    {
        Direction();
    }
    
    // Muda a direção do scorpio de acordo com o lado que o player se encontra
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

    void ShootRight()
    {
        scorpioPoison.Shoot(scorpioManager.poisonPointRight, scorpioPoison.gameObject);
    }

    void ChangeAnimation()
    {
        animator.SetBool("Attack", false);
    }
    void ShootLeft()
    {
        scorpioPoison.Shoot(scorpioManager.poisonPointLeft, scorpioPoison.gameObject);
    }
}
