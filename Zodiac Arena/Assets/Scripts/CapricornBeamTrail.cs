using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapricornBeamTrail : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    private Vector2 startingPoint;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = speed * transform.right;
        StartCoroutine(DestroyTrail(2f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("BeamSpawn"))
        {
            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        }
    }

    private IEnumerator DestroyTrail (float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
}
