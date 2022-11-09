using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capricorn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject beam;
    [SerializeField] private float velocity;
    [SerializeField] private int nShots;
    private Transform beamSpawn;
    private Rigidbody2D rb;
    private Animator anim;
    private bool facedRight;
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
        /*if (Input.GetKeyDown("left shift"))
        {
            ShootLaserBeam();
        }*/
        if (running)
        {
            //RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, player.position - beamSpawn.position,) //para jump
            if(facedRight && transform.position.x > 6.5)
            {
                rb.AddForce(Vector2.left * velocity);
                running = false;
                StartCoroutine(StartAttacks(1f));
            }
            if(!facedRight && transform.position.x < -6.5)
            {
                rb.AddForce(Vector2.right * velocity);
                running = false;
                StartCoroutine(StartAttacks(1f));
            }
            if(player.position.x - transform.position.x < 0)
            {
                if (facedRight)
                {
                    rb.AddForce(Vector2.left * velocity);
                    running = false;
                    StartCoroutine(StartAttacks(1f));
                }
            }
            else
            {
                if (!facedRight)
                {
                    rb.AddForce(Vector2.right * velocity);
                    running = false;
                    StartCoroutine(StartAttacks(1f));
                }
            }
        }
    }

    void ShootLaserBeam()
    {
        beamSpawn.right = (player.position - beamSpawn.position).normalized;
        Instantiate(beam, beamSpawn.position, beamSpawn.rotation);
    }

    public void Assault() // Trigger do fim da animação vai chamar esta funcao
    {
        running = true;
        if (player.position.x - transform.position.x > 0)
        {
            rb.AddForce(Vector2.right * velocity);
            facedRight = true;
        }
        else
        {
            rb.AddForce(Vector2.left * velocity);
            facedRight = false;
        }
    }


    private IEnumerator StartAttacks(float startTime)
    {
        yield return new WaitForSeconds(startTime);
        for (int i = 0; i < nShots; i++)
        {
            ShootLaserBeam();
            yield return new WaitForSeconds(1.5f);
        }
        yield return new WaitForSeconds(0.5f);
        anim.SetTrigger("charge");
    }
}
