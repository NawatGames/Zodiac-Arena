using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ScorpioPoison : MonoBehaviour
{
    public float speedX = 5f;
    public float speedY = 5f;
    public Rigidbody2D rb;
    
    public GroundState ground;
    public GameObject poisongroundPrefab;
    
    void Start()
    {
        PosionMove(rb,speedX, speedY);
        ground = GameObject.FindWithTag("ground").GetComponent<GroundState>();
        Debug.Log(ground);
    }
    
    // Criação/Instanciação do gameobject veneno
    public void Shoot(Transform poisonPoint, GameObject poisonPrefab )
    {
        Instantiate(poisonPrefab, poisonPoint.position, poisonPoint.rotation);
    }

    // Movimentação em segundo grau do veneno
    public void PosionMove(Rigidbody2D rb, float speed, float speed2)
    {
        rb.velocity = transform.right * Random.Range(0, speed) + transform.up * Random.Range(0, speed2);
    }

    // Quando entrar em contato com qualquer gameObject diferente da Scorpio, o veneno é destruido
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Scorpio") 
        {
            Destroy(gameObject);
        }

        if (other.tag == "ground")
        {
            ground.PoisonGround(poisongroundPrefab, this.transform);
        }
    }
}
