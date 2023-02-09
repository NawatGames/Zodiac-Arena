using System;
using System.Collections;
using UnityEngine;

public class ScorpioManager : MonoBehaviour
{
    [Header("Atributos do Veneno Lançado pela Scorpio")]
    public Transform poisonPointRight;
    public Transform poisonPointLeft;
    public ScorpioPoison scorpioPoison;
    public float timeinseconds;
    private bool isCoroutineExecuting;

    private Ray ray;
    
    public Animator animator;

    void Update()
    {
        StartCoroutine(ExecuteAfterTime(timeinseconds));

    }
    // Atacar após um tempo
    IEnumerator ExecuteAfterTime(float time)
    {
        if (isCoroutineExecuting || animator.GetCurrentAnimatorStateInfo(0).IsName("attack_left") || animator.GetCurrentAnimatorStateInfo(0).IsName("attack_right"))
            yield break;
        
        isCoroutineExecuting = true;

        yield return new WaitForSeconds(time);

        if (animator.GetBool("Direction") == true)
        {
            animator.SetBool("Attack", true);
            

        }
        if (animator.GetBool("Direction") == false)
        {
            animator.SetBool("Attack", true);
        }
        
        isCoroutineExecuting = false;
    }

    //Quando o Player entrar nessa área, ele morre
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            animator.SetBool("Melee", true);
        }
    }
    
}
