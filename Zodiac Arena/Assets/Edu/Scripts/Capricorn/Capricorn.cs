using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capricorn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject beam;
    [SerializeField] private float velocity;
    [SerializeField] private int nShots;
    [SerializeField] private AnimatorOverrideController leftAnimCtrl;
    [SerializeField] private AnimatorOverrideController rightAnimCtrl;
    private Transform beamSpawn;
    private Rigidbody2D rb;
    private Animator anim;
    private bool facingRight = false;
    private bool running = false;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 6);

        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        beamSpawn = transform.GetChild(0);
        StartCoroutine(StartAttacks(1.5f));
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.x - transform.position.x > 0)
        {
            if (!facingRight)
            {
                if (running)
                {
                    StopAssault(Vector2.right);
                }
                else
                {
                    anim.runtimeAnimatorController = rightAnimCtrl;
                    facingRight = true;
                }
            }
        }
            
        else
        {
            if (facingRight)
            {
                if (running)
                {
                    StopAssault(Vector2.left);
                }
                else
                {
                    anim.runtimeAnimatorController = leftAnimCtrl;
                    facingRight = false;
                }
            }
        }
    

        if (running)
        {
            if(facingRight && transform.position.x > 7)
            {
                StopAssault(Vector2.left);
            }
            if(!facingRight && transform.position.x < -7)
            {
                StopAssault(Vector2.right);
            }
        }
    }

    public void ShootLaserBeam()
    {
        beamSpawn.right = (player.position - beamSpawn.position).normalized;
        Instantiate(beam, beamSpawn.position, beamSpawn.rotation);
    }

    public void Assault() // Trigger do fim da animação de charge chama esta funcao
    {
        running = true;
        if (facingRight)
        {
            rb.AddForce(Vector2.right * velocity);
        }
        else
        {
            rb.AddForce(Vector2.left * velocity);
        }
    }

    private void StopAssault(Vector2 stoppingForceDirection)
    {
        anim.SetTrigger("stop");
        rb.AddForce(stoppingForceDirection * velocity);
        running = false;
        StartCoroutine(StartAttacks(1f));
    }


    private IEnumerator StartAttacks(float startTime)
    {
        yield return new WaitForSeconds(startTime);
        for (int i = 0; i < nShots; i++)
        {
            anim.SetTrigger("shoot");
            yield return new WaitForSeconds(2f);
        }
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("charge");
    }
}
