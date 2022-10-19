using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AriesGroundRam : MonoBehaviour
{
    private Rigidbody2D rb;
    private float velocity;
    public bool goRight = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyThis(3f));
        rb = GetComponent<Rigidbody2D>();
        velocity = Random.Range(450f, 650f);

        if (goRight)
            rb.AddForce(Vector2.right * velocity);
        else
            rb.AddForce(Vector2.left * velocity);
    }

    private IEnumerator DestroyThis(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
