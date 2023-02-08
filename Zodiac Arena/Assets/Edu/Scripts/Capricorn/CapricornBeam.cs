using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapricornBeam : MonoBehaviour
{
    [SerializeField] private int maxBounces;
    private int nBounces;
    [SerializeField] private float lifetime;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private bool destroy = false;
    ////[SerializeField] private GameObject beamTrail;
    ////private bool started = false;
    ////private bool spawnComplete = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed * transform.right;
        StartCoroutine(Lifetime(lifetime));
        ////started = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        ////if (!started)
        ////    return;
        ////if (!spawnComplete)
        ////    if (Vector2.Dot(col.bounds.ClosestPoint(transform.position) - transform.position, transform.right) < 0)
        ////        return;

        if (destroy || nBounces == maxBounces)
        {
            StartCoroutine(DestroyBeam(2f));
            return; // Impede que haja reflexão
        }

        ////GameObject beamInstance;

        if (col.CompareTag("HorizontalEdge"))
        {
            nBounces++;
            ////beamInstance = Instantiate(beamTrail, transform.position, transform.rotation);
            transform.right *= new Vector2(1, -1);
            /*if (transform.right.y > 0.4 || transform.right.y < 0.4)
                transform.Translate(-transform.right.y * transform.up);*/
            rb.velocity = speed * transform.right;
            ////if (spawnComplete)
            ////    beamInstance.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        }
        else if (col.CompareTag("VerticalEdge"))
        {
            nBounces++;
            ////beamInstance = Instantiate(beamTrail, transform.position, transform.rotation);
            transform.right *= new Vector2(-1, 1);
            rb.velocity = speed * transform.right;
            ////if (spawnComplete)
            ////    beamInstance.GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        }
    }

    ////void OnTriggerExit2D(Collider2D col)
    ////{
    ////    if(col.CompareTag("BeamSpawn"))
    ////    {
    ////        GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
    ////        spawnComplete = true;
    ////    }
    ////}

    private IEnumerator Lifetime(float duration)
    {
        yield return new WaitForSeconds(duration);
        destroy = true;
    }

    private IEnumerator DestroyBeam(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
