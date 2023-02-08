using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AriesKnockbackRam : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private bool dashing = true;
    private bool spawnedFromRight = false;
    public PlayerMovementController playerScript;
    [HideInInspector] public Transform player;
    [SerializeField] private float velocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (transform.position.x > 0)
        {
            spawnedFromRight = true;
            transform.Rotate(0, 180, 0);
        }
    }

    private void Update()
    {
        if (dashing)
        {
            transform.right = (player.position - transform.position).normalized;
            if (spawnedFromRight && transform.rotation.y == 0)
                transform.Rotate(0, 180, 180);
            rb.velocity = transform.right * velocity;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") || col.CompareTag("dodging"))
        {
            playerScript.ApplyKnockBack(transform.right);
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
