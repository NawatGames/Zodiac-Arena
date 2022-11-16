using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taurus : MonoBehaviour
{
    [SerializeField] private float jumpDuration;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject shockWavePrefab;
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private Transform projectileOrigin;
    [SerializeField] private int nProjectiles;
    [SerializeField] private float throwDelay;
    [SerializeField] private float waveProjectile_gap;
    private Rigidbody2D rb;
    private float playerPosX;
    private float halfTaurusHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        halfTaurusHeight = transform.lossyScale.y / 2;
        // Player vira comida
        StartCoroutine(StartAttacks());
    }

    private IEnumerator StartAttacks()
    {
        while(true)
        {
            for(int i=0; i < nProjectiles; i++)
            {
                projectileOrigin.right = player.position - projectileOrigin.position;
                //animação de lançar objeto (chamar instantiate no fim da animação?)
                Instantiate(knifePrefab, projectileOrigin.position, projectileOrigin.rotation);
                yield return new WaitForSeconds(throwDelay);
            }
            // animação de pulo
            yield return new WaitForSeconds(1f);
            playerPosX = player.position.x;
            StartCoroutine(JumpToPos(playerPosX, jumpHeight, jumpDuration));
            yield return new WaitForSeconds(waveProjectile_gap);
        }
    }

    private IEnumerator JumpToPos (float  distance, float height, float duration)
    {
        float t = 0;
        float posX = transform.position.x;
        float posY = transform.position.y;
        while(t < 1)
        {
            t += Time.fixedDeltaTime/duration;
            rb.MovePosition(new Vector2(Mathf.Lerp(posX, playerPosX, t), Mathf.Lerp(posY, height, 4 * (-t) * (t - 1))));

            yield return new WaitForFixedUpdate();
        }
        Instantiate(shockWavePrefab, new Vector2(transform.position.x, transform.position.y - halfTaurusHeight), transform.rotation);
    }
}
