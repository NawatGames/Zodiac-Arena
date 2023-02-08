using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aries : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private GameObject groundRam;
    [SerializeField] private GameObject jumpingRam;
    [SerializeField] private GameObject fallingRam;
    ////////////////////[SerializeField] private GameObject KnockbackRam; (Está no script do player)
    [SerializeField] private int nFallingRams; //   Numero total de Rams = 2*nFallingRams
    [SerializeField] private GameObject[] KnockbackZones; // {esquerda, centro, direita}
    [SerializeField] private AnimatorOverrideController leftAnimCtrl;
    [SerializeField] private AnimatorOverrideController rightAnimCtrl;
    ////////////////////[SerializeField] private int kbIntensity;
    /////////////////////private playerr playerScript; // NOME ERRADO
    private Animator anim;
    private bool facingRight = false;
    private bool middlePos = false;
    private float spawnDelay;
    private const float ramRightSpawnX = 9.7f;
    private const float ramLeftSpawnX = -9.7f;
    private const float spawnY = -4f;
    private const float AriesPosY = -4f;
    private float[] AriesPosX = { -7, 0, 7 };
    private int posIndex = 2;

    void Awake()
    {
        Physics2D.IgnoreLayerCollision(8, 13);
        Physics2D.IgnoreLayerCollision(8, 6);
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        /////////////////////playerScript = player.GetComponent<playerr>();
        StartCoroutine(StartingGame(1f));
        spawnDelay = 1f;
    }

    private void Update()
    {
        if(middlePos)
        {
            if (player.position.x - transform.position.x > 0)
            {
                if (facingRight == false)
                {
                    facingRight = true;
                    anim.runtimeAnimatorController = rightAnimCtrl;
                }
            }
            else
            {
                if (facingRight == true)
                {
                    facingRight = false;
                    anim.runtimeAnimatorController = leftAnimCtrl;
                }
            }
        }
    }

    private IEnumerator StartingGame(float wait)
    {
        yield return new WaitForSeconds(wait);
        //IF TIME SCALE != 0 -> SET TRIGGER
        anim.SetTrigger("battleCry");
    }

    private IEnumerator SpawnFallingRams (float spawnCooldown, int ramsMultiplier)
    {
        Vector2 spawnPos;
        spawnPos.y = 5.8f;
        for(int i = 0; i < nFallingRams * ramsMultiplier; i++)
        {
            spawnPos.x = Random.Range(-7.4f,7.4f);
            GameObject obj = Instantiate(fallingRam, spawnPos, Quaternion.identity);
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

    public void StartAttacks() // Trigger da animação "battlecry" chamará esta função
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
    public void ChangeDirectionAndTP() // fim da animação tpOut chama essa função
    {
        KnockbackZones[posIndex].SetActive(false);
        Debug.Log("pos atual = " + posIndex);
        posIndex = (posIndex + Random.Range(1, 3)) % 3;
        Debug.Log("nova pos = " + posIndex);
        switch (posIndex)
        {
            case 0:
                middlePos = false;
                facingRight = true;
                anim.runtimeAnimatorController = rightAnimCtrl;
                break;
            case 1:
                middlePos = true;
                break;
            case 2:
                middlePos = false;
                facingRight = false;
                anim.runtimeAnimatorController = leftAnimCtrl;
                break;
            default:
                break;
        }
        transform.position = new Vector2(AriesPosX[posIndex], AriesPosY);
        KnockbackZones[posIndex].SetActive(true);
    }
}
