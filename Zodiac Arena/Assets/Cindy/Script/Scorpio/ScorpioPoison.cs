using System;
using UnityEngine;

public class ScorpioPoison : MonoBehaviour
{
    public float speedX = 5f;
    public float speedY = 5f;
    public Rigidbody2D rb;
    void Start()
    {
        PosionMove(rb,speedX, speedY);
    }
    public void Shoot(Transform poisonPoint, GameObject poisonPrefab )
    {
        Instantiate(poisonPrefab, poisonPoint.position, poisonPoint.rotation);
    }

    public void PosionMove(Rigidbody2D rb, float speed, float speed2)
    {
        rb.velocity = transform.right * speed + transform.up * speed2;
        Debug.Log(rb.velocity);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name != "Scorpio") 
        {
            Debug.Log(other.name);
            Destroy(gameObject);
        }
    }
}
