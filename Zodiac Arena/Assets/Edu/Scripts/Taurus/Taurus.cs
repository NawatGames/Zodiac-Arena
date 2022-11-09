using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Taurus : MonoBehaviour
{
    [SerializeField] private float jumpDuration;
    [SerializeField] private float jumpHeight;
    [SerializeField] private Transform player;
    private Rigidbody2D rb;
    private float playerPosX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(StartAttacks());
    }

    private IEnumerator StartAttacks()
    {
        while(true)
        {
            // lança objetos
            // animação de pulo
            yield return new WaitForSeconds(1f);
            playerPosX = player.position.x;
            StartCoroutine(JumpToPos(playerPosX, jumpHeight, jumpDuration));
            yield return new WaitForSeconds(3f);
        }
    }

    private IEnumerator JumpToPos (float  distance, float height, float duration)
    {
        bool done = false;
        float t = 0;
        float posX = transform.position.x;
        float posY = transform.position.y;
        while(t < 1)
        {
            t += Time.fixedDeltaTime/duration;
            rb.MovePosition(new Vector2(Mathf.Lerp(posX, playerPosX, t), Mathf.Lerp(posY, height, 4 * (-t) * (t - 1))));

            yield return new WaitForFixedUpdate();
        }
    }
}
