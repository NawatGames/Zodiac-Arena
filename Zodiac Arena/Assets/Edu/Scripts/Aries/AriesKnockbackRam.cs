using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AriesKnockbackRam : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool dashing = true;
    public playerr playerScript;
    [HideInInspector] public Transform player;
    [SerializeField] private float velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (dashing)
        {
            transform.right = (player.position - transform.position).normalized;
            rb.velocity = transform.right * velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerScript.ApplyKnockBack(transform.right, 800);
            dashing = false;
            rb.velocity = Vector2.zero;
            anim.SetTrigger("fade");
        }
    }

    public void DestroyThis()
    {
        Destroy(gameObject);
    }
}
