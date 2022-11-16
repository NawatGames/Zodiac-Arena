using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaurusProjectiles : MonoBehaviour
{
    [SerializeField] private int spinMultiplier; // 0 para não girar
    [SerializeField] private float velocity;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(velocity * transform.right);
        rb.AddTorque(spinMultiplier * 300f);
    }
}
