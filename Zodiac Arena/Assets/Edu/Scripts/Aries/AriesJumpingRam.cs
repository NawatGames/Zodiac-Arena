using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AriesJumpingRam : MonoBehaviour
{
    private Rigidbody2D rb;
    private float velocity;
    public bool goRight = true;
    private float walkTime;
    private float gScale;
    private const int S = 850;
    private const int H = 400;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyThis(3f));
        rb = GetComponent<Rigidbody2D>();

        velocity = Random.Range(450f, 650f);
        walkTime = Random.Range(Mathf.Lerp(0.6f,1f,(450-velocity)/200f), Mathf.Lerp(1.1f, 1.6f, (450 - velocity) / 200f));
        //      min = 0.6f com v = 650 ; min = 1f com v = 450
        //      max = 1.1f com v = 650 ; max = 1.6f com v = 450

        gScale = 4f/520 /(Mathf.Pow((S-velocity*walkTime)/velocity,2f)/ (2 * H * (S - velocity * walkTime) / 1000f));
        rb.gravityScale = gScale;

        if (goRight)
        {
            rb.AddForce(Vector2.right * velocity);
            transform.Rotate(0, 180, 0);
        }
        else
        {
            rb.AddForce(Vector2.left * velocity);
        }

        StartCoroutine(WaitAndJump(walkTime));
    }

    //                                              g = gScale * 520
    private IEnumerator WaitAndJump(float wait)  // g/2 = gScale * 260      // S = 850 = 500 * 1.7 = Vmax * t_max (qnd encostando a parede)
    {
        yield return new WaitForSeconds(wait);
        //                       (g/2)     *  (S   -    v    *  t)  /   v
        rb.AddForce(((gScale * 260) * (S - velocity * wait)/velocity) * Vector2.up);
    }

    private IEnumerator DestroyThis(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
