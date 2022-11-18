using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EspadasDoJulgamento : MonoBehaviour
{
    public GameObject sword;
    public GameObject warning;
    public GameObject leftWall;
    public GameObject rightWall;
    public float warningDelay = 1.5f;
    public float swordVelocity = 5f;
    public float swordDelay = 0f;
    // void Start()
    // {
    //     StartCoroutine(ThrowUpSword());
    // }

    public IEnumerator ThrowUpSword()
    {
        var posicao = Random.Range(leftWall.transform.position.x, rightWall.transform.position.x);
        // Debug.Log(posicao);
        var warningInstantiate = Instantiate(warning, new Vector3(posicao, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(warningDelay);
        Destroy(warningInstantiate);
        var swordInstantiate = Instantiate(sword, new Vector3(posicao, 4.5f, 0), Quaternion.identity);
        swordInstantiate.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -swordVelocity);
        yield return new WaitForSeconds(swordDelay);
        StartCoroutine(ThrowUpSword());
        yield return new WaitForSeconds(3f);
        Destroy(swordInstantiate);
        
    }
    public IEnumerator ThrowLeftSword()
    {
        var posicao = Random.Range(leftWall.transform.position.x, rightWall.transform.position.x);
        // Debug.Log(posicao);
        var warningInstantiate = Instantiate(warning, new Vector3(posicao, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(warningDelay);
        Destroy(warningInstantiate);
        var swordInstantiate = Instantiate(sword, new Vector3(posicao, 4.5f, 0), Quaternion.identity);
        swordInstantiate.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -swordVelocity);
        yield return new WaitForSeconds(swordDelay);
        StartCoroutine(ThrowUpSword());
        yield return new WaitForSeconds(3f);
        Destroy(swordInstantiate);
    }
    public IEnumerator ThrowRightSword()
    {
        var posicao = Random.Range(leftWall.transform.position.x, rightWall.transform.position.x);
        // Debug.Log(posicao);
        var warningInstantiate = Instantiate(warning, new Vector3(posicao, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(warningDelay);
        Destroy(warningInstantiate);
        var swordInstantiate = Instantiate(sword, new Vector3(posicao, 4.5f, 0), Quaternion.identity);
        swordInstantiate.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -swordVelocity);
        yield return new WaitForSeconds(swordDelay);
        StartCoroutine(ThrowUpSword());
        yield return new WaitForSeconds(3f);
        Destroy(swordInstantiate);
    }
    public IEnumerator ThrowSideSword()
    {
        var posicao = Random.Range(leftWall.transform.position.x, rightWall.transform.position.x);
        // Debug.Log(posicao);
        var warningInstantiate = Instantiate(warning, new Vector3(posicao, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(warningDelay);
        Destroy(warningInstantiate);
        var swordInstantiate = Instantiate(sword, new Vector3(posicao, 4.5f, 0), Quaternion.identity);
        swordInstantiate.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -swordVelocity);
        yield return new WaitForSeconds(swordDelay);
        StartCoroutine(ThrowUpSword());
        yield return new WaitForSeconds(3f);
        Destroy(swordInstantiate);
    }
}
