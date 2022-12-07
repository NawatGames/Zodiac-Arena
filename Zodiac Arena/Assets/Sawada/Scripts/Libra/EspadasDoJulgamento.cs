using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EspadasDoJulgamento : MonoBehaviour
{
    public GameObject sword;
    public GameObject warningVertical;
    public GameObject warningHorizontal;
    public GameObject leftWall;
    public GameObject rightWall;
    public GameObject upWall;
    public GameObject downWall;
    public float startDelay = 3f;
    public float warningDelay = 1.5f;
    public float swordVelocity = 5f;
    public float swordDelay = 0f;
    public float atackDuration = 10f;
    public Balanca balanca;
    public bool isStartDelay = false;
    private float startTime = 0f;
    
    public IEnumerator ThrowUpSword()
    {
        if (!isStartDelay)
        {
            isStartDelay = true;
            yield return new WaitForSeconds(startDelay);
            startTime = Time.time;
        }
        var posicao = Random.Range(leftWall.transform.position.x, rightWall.transform.position.x);
        var warningInstantiate = Instantiate(warningVertical, new Vector3(posicao, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(warningDelay);
        Destroy(warningInstantiate);
        var swordInstantiate = Instantiate(sword, new Vector3(posicao, upWall.transform.position.y, 0), Quaternion.identity);
        swordInstantiate.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -swordVelocity);
        yield return new WaitForSeconds(swordDelay);
        if(Time.time - startTime <= atackDuration)
            StartCoroutine(ThrowUpSword());
        else
            balanca.StartAttack();
        yield return new WaitForSeconds(3f);
        Destroy(swordInstantiate);
    }
    public IEnumerator ThrowLeftSword()
    {
        if (!isStartDelay)
        {
            isStartDelay = true;
            yield return new WaitForSeconds(startDelay);
            startTime = Time.time;
        }
        var posicao = Random.Range(downWall.transform.position.y, upWall.transform.position.y);
        var warningInstantiate = Instantiate(warningHorizontal, new Vector3(0, posicao, 0),Quaternion.AngleAxis(90, Vector3.forward));
        yield return new WaitForSeconds(warningDelay);
        Destroy(warningInstantiate);
        var swordInstantiate = Instantiate(sword, new Vector3(leftWall.transform.position.x, posicao, 0), Quaternion.AngleAxis(90, Vector3.forward));
        swordInstantiate.GetComponent<Rigidbody2D>().velocity = new Vector2(swordVelocity, 0);
        yield return new WaitForSeconds(swordDelay);
        if(Time.time - startTime <= atackDuration)
            StartCoroutine(ThrowLeftSword());
        else
            balanca.StartAttack();
        yield return new WaitForSeconds(3f);
        Destroy(swordInstantiate);
    }
    public IEnumerator ThrowRightSword()
    {
        if (!isStartDelay)
        {
            isStartDelay = true;
            yield return new WaitForSeconds(startDelay);
            startTime = Time.time;
        }
        var posicao = Random.Range(downWall.transform.position.y, upWall.transform.position.y);
        var warningInstantiate = Instantiate(warningHorizontal, new Vector3(0, posicao, 0), Quaternion.AngleAxis(90, Vector3.forward));
        yield return new WaitForSeconds(warningDelay);
        Destroy(warningInstantiate);
        var swordInstantiate = Instantiate(sword, new Vector3(rightWall.transform.position.x, posicao, 0), Quaternion.AngleAxis(90, Vector3.back));
        swordInstantiate.GetComponent<Rigidbody2D>().velocity = new Vector2(-swordVelocity, 0);
        yield return new WaitForSeconds(swordDelay);
        if(Time.time - startTime <= atackDuration)
            StartCoroutine(ThrowRightSword());
        else
            balanca.StartAttack();
        yield return new WaitForSeconds(3f);
        Destroy(swordInstantiate);
    }
    public IEnumerator ThrowSideSword()
    {
        if (!isStartDelay)
        {
            isStartDelay = true;
            yield return new WaitForSeconds(startDelay);
            startTime = Time.time;
        }
        var posicao1 = Random.Range(downWall.transform.position.y, upWall.transform.position.y);
        var posicao2 = Random.Range(downWall.transform.position.y, upWall.transform.position.y);
        var warningInstantiate1 = Instantiate(warningHorizontal, new Vector3(0, posicao1, 0), Quaternion.AngleAxis(90, Vector3.forward));
        var warningInstantiate2 = Instantiate(warningHorizontal, new Vector3(0, posicao2, 0), Quaternion.AngleAxis(90, Vector3.forward));
        yield return new WaitForSeconds(warningDelay);
        Destroy(warningInstantiate1);
        Destroy(warningInstantiate2);
        var swordInstantiateLeft = Instantiate(sword, new Vector3(leftWall.transform.position.x, posicao1, 0), Quaternion.AngleAxis(90, Vector3.forward));
        swordInstantiateLeft.GetComponent<Rigidbody2D>().velocity = new Vector2(swordVelocity, 0);
        var swordInstantiateRight = Instantiate(sword, new Vector3(rightWall.transform.position.x, posicao2, 0),  Quaternion.AngleAxis(90, Vector3.back));
        swordInstantiateRight.GetComponent<Rigidbody2D>().velocity = new Vector2(-swordVelocity, 0);
        yield return new WaitForSeconds(swordDelay);
        if(Time.time - startTime <= atackDuration)
            StartCoroutine(ThrowSideSword());
        else
            balanca.StartAttack();
        yield return new WaitForSeconds(3f);
        Destroy(swordInstantiateLeft);
        Destroy(swordInstantiateRight);
    }
}
