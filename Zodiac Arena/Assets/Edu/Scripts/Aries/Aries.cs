using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aries : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject groundRam;
    [SerializeField] private GameObject jumpingRam;
    [SerializeField] private GameObject fallingRam;
    [SerializeField] private int nFallingRams; //   Numero total de Rams = 2*nFallingRams
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private bool facingRight;
    private float spawnDelay;
    private const float ramRightSpawnX = 9.7f;
    private const float ramLeftSpawnX = -9.7f;
    private const float spawnY = -4f;
    private float[] AriesPos = { 7, 0, -7 };
    private int posIndex = 2;

    void Awake()
    {
        Physics2D.IgnoreLayerCollision(6, 13);
        Physics2D.IgnoreLayerCollision(6, 6);
        Physics2D.IgnoreLayerCollision(4, 6);  // layer 4 eh a layer "Water" -> criar nova layer no lugar de 4
        Physics2D.IgnoreLayerCollision(4, 13);
        Physics2D.IgnoreLayerCollision(4, 12);
        Physics2D.IgnoreLayerCollision(4, 4);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        facingRight = false;
        StartCoroutine(StartingGame(1f));
        spawnDelay = 1f;
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    private IEnumerator StartingGame(float wait)
    {
        yield return new WaitForSeconds(wait);
        anim.SetTrigger("battleCry");
    }

    private IEnumerator SpawnFallingRams (float spawnCooldown)
    {
        Vector2 spawnPos;
        spawnPos.y = 5.8f;
        for(int i = 0; i < nFallingRams; i++)
        {
            spawnPos.x = Random.Range(-7.4f,7.4f);
            GameObject obj = Instantiate(fallingRam, spawnPos, transform.rotation);
            obj.GetComponent<AriesFallingRam>().playerPos = player.position;
            yield return new WaitForSeconds(spawnCooldown);
        }
    }
    private IEnumerator SpawnRams (int remainingGrounds,int remainingJumping, bool goRight, float spawnCooldown)
    {
        StartCoroutine(SpawnFallingRams(spawnCooldown));
        float spawnX;
        if (goRight)
            spawnX = ramLeftSpawnX;
        else
            spawnX = ramRightSpawnX;

        while(remainingGrounds * remainingJumping > 0)
        {
            if(Random.Range(0,2) == 0)
            {
                GameObject obj = Instantiate(groundRam, new Vector2(spawnX, spawnY), transform.rotation);
                obj.GetComponent<AriesGroundRam>().goRight = goRight;
                remainingGrounds--;
            }
            else
            {
                GameObject obj = Instantiate(jumpingRam, new Vector2(spawnX, spawnY), transform.rotation);
                obj.GetComponent<AriesJumpingRam>().goRight = goRight;
                remainingJumping--;
            }
            yield return new WaitForSeconds(spawnCooldown);
        }
        while(remainingGrounds > 0)
        {
            GameObject obj = Instantiate(groundRam, new Vector2(spawnX, spawnY), transform.rotation);
            obj.GetComponent<AriesGroundRam>().goRight = goRight;
            remainingGrounds--;
            yield return new WaitForSeconds(spawnCooldown);
        }
        while(remainingJumping > 0)
        {
            GameObject obj = Instantiate(jumpingRam, new Vector2(spawnX, spawnY), transform.rotation);
            obj.GetComponent<AriesJumpingRam>().goRight = goRight;
            remainingJumping--;
            yield return new WaitForSeconds(spawnCooldown);
        }
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("tpOut");
    }


    public void StartAttacks() // Trigger do fim da animação chamará esta função
    {
        int nGround = Random.Range(1, 6) * nFallingRams / 10;
        int nJumping = nFallingRams - nGround;

        StartCoroutine(SpawnRams(nGround,nJumping,facingRight,spawnDelay));
        
    }
    public void ChangeDirectionAndTP()
    {
        facingRight = !facingRight;
        spriteRenderer.flipY = !spriteRenderer.flipY;
        posIndex = (posIndex + Random.Range(1, 3)) % 3;
        transform.position = new Vector2(AriesPos[posIndex], transform.position.y);
    }
}
