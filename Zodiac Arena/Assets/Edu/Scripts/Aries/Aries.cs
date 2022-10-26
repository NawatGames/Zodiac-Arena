using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aries : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject groundRam;
    [SerializeField] private GameObject jumpingRam;
    [SerializeField] private GameObject fallingRam;
    ////////////////////[SerializeField] private GameObject KnockbackRam;
    [SerializeField] private int nFallingRams; //   Numero total de Rams = 2*nFallingRams
    [SerializeField] private GameObject[] KnockbackZones; // {esquerda, centro, direita}
    ////////////////////[SerializeField] private int kbIntensity;
    /////////////////////private playerr playerScript; // NOME ERRADO
    private Animator anim;
    private SpriteRenderer spriteRenderer;
    private float spawnDelay;
    private const float ramRightSpawnX = 9.7f;
    private const float ramLeftSpawnX = -9.7f;
    private const float spawnY = -4f;
    private const float AriesPosY = -3.885f;
    private float[] AriesPosX = { -7, 0, 7 };
    private int posIndex = 2;

    void Awake()
    {
        Physics2D.IgnoreLayerCollision(6, 13);
        Physics2D.IgnoreLayerCollision(6, 6);
        Physics2D.IgnoreLayerCollision(7, 6);
        Physics2D.IgnoreLayerCollision(7, 13);
        Physics2D.IgnoreLayerCollision(7, 12);
        Physics2D.IgnoreLayerCollision(7, 7);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        /////////////////////playerScript = player.GetComponent<playerr>();
        StartCoroutine(StartingGame(1f));
        spawnDelay = 1f;
    }

    /*private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            Vector2 kbOrigin;
            if (posIndex == 1)
            {
                kbOrigin.x = 2 * (player.position.x <= 0 ? 1 : -1);
                kbOrigin.y = AriesPosY;
            }
            else
            {
                kbOrigin.x = AriesPosX[posIndex] * 1.3f;
                kbOrigin.y = player.position.y; // se y > altura do chao, kb.y pode ser um pouco mais baixo p mandar mais p cima
            }
            Vector2 dir = (new Vector2(player.position.x, player.position.y) - kbOrigin).normalized;
            GameObject obj = Instantiate(KnockbackRam,kbOrigin,transform.rotation);
            obj.GetComponent<AriesKnockbackRam>().direction = dir;
            playerScript.ApplyKnockBack(dir, kbIntensity);
        }
    }*/

    private IEnumerator StartingGame(float wait)
    {
        yield return new WaitForSeconds(wait);
        anim.SetTrigger("battleCry");
    }

    private IEnumerator SpawnFallingRams (float spawnCooldown, int ramsMultiplier)
    {
        Vector2 spawnPos;
        spawnPos.y = 5.8f;
        for(int i = 0; i < nFallingRams * ramsMultiplier; i++)
        {
            spawnPos.x = Random.Range(-7.4f,7.4f);
            GameObject obj = Instantiate(fallingRam, spawnPos, transform.rotation);
            obj.GetComponent<AriesFallingRam>().playerPos = player.position;
            yield return new WaitForSeconds(spawnCooldown);
        }
        yield return new WaitForSeconds(2f);
        anim.SetTrigger("tpOut");
    }
    private IEnumerator SpawnAllRams (int remainingGrounds,int remainingJumping, bool goRight, float spawnCooldown)
    {
        StartCoroutine(SpawnFallingRams(spawnCooldown,1));
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
        // Animação de tp para recomeçar ataque é ativada no fim de SpawnFallingRams()
    }

    public void StartAttacks() // Trigger do fim da animação chamará esta função
    {
        int nGround = Random.Range(1, 6) * nFallingRams / 10;
        int nJumping = nFallingRams - nGround;

        switch(posIndex)
        {
            case 0:
                StartCoroutine(SpawnAllRams(nGround, nJumping, true, spawnDelay));
                break;
            case 1:
                StartCoroutine(SpawnFallingRams(spawnDelay / 2, 2));
                break;
            case 2:
                StartCoroutine(SpawnAllRams(nGround, nJumping, false, spawnDelay));
                break;
            default:
                break;
        }
    }
    public void ChangeDirectionAndTP()
    {
        KnockbackZones[posIndex].SetActive(false);
        //spriteRenderer.flipY = !spriteRenderer.flipY;
        posIndex = (posIndex + Random.Range(1, 3)) % 3;
        transform.position = new Vector2(AriesPosX[posIndex], AriesPosY);
        KnockbackZones[posIndex].SetActive(true);
    }
}
