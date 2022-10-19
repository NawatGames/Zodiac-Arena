using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AriesFallingRam : MonoBehaviour
{
    private Rigidbody2D rb;
    private float velocity;
    private Vector2 direction;
    public Vector2 playerPos;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroyThis(3f));
        rb = GetComponent<Rigidbody2D>();
        direction = playerPos - new Vector2 (transform.position.x, transform.position.y);
        velocity = Random.Range(12f,15f);
        rb.AddForce(new Vector2 (direction.x, 25f).magnitude * velocity * direction.normalized);
       
    }

    private IEnumerator DestroyThis(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
