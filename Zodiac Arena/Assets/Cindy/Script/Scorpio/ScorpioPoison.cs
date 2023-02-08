using System;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ScorpioPoison : MonoBehaviour
{
    [Header("Atributos do Veneno")]
    public float speedX = 5f;
    public float speedY = 5f;
    public Rigidbody2D rb;
    
    [Header("Atributos do Chão do Veneno")]
    public GroundState ground;
    public GameObject poisongroundPrefab;

    public GameObject player;
    public GameObject scorpio;

    private void Awake()
    {
        player = GameObject.Find("Player");
        scorpio = GameObject.Find("Scorpio");
    }

    void Start()
    {
        PosionMove(rb,speedX, speedY);
        ground = GameObject.FindWithTag("ground").GetComponent<GroundState>(); // Acha o objeto "ground"
    }
    
    // Criação/Instanciação do gameobject veneno
    public void Shoot(Transform poisonPoint, GameObject poisonPrefab )
    {
        Instantiate(poisonPrefab, poisonPoint.position, poisonPoint.rotation);
    }

    // Movimentação em segundo grau do veneno, seguindo o player 
    public void PosionMove(Rigidbody2D rb, float speed, float speed2)
    {
        print(player.transform.position);
        Vector2 distance = player.transform.position - scorpio.transform.position;
        rb.velocity = transform.right * distance.x * Random.Range(0, speed) +
                      transform.up * distance.y * Random.Range(0, speed2);
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        // Quando entrar em contato com qualquer gameObject diferente da Scorpio, o veneno é destruido
        if (other.name != "Scorpio") 
        {
            Destroy(gameObject);
        }

        // Quando entrar em contato com um objeto de tag "ground" ele cria um novo gameobject
        if (other.tag == "ground" )
        {
            ground.PoisonGround(poisongroundPrefab, this.transform);
        }
    }
}
