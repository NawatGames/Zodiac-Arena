using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    public GameObject light;
    public float timeinsecondsenablelight = 2;

    private bool isCoroutineExecuting;
    public void Update()
    {
        StartCoroutine(ExecuteAfterTimeEnable(timeinsecondsenablelight));
    }

    // Após um tempo randômico ativa a luz e depois de outro tempo randomico desativa o espelho e a luz
    IEnumerator ExecuteAfterTimeEnable(float time)
    {
        if (isCoroutineExecuting)
            yield break;
        
        isCoroutineExecuting = true;
        
        yield return new WaitForSeconds(time);
        light.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(Random.Range(0,time));
        light.gameObject.SetActive(false);
        this.gameObject.SetActive(false);

        isCoroutineExecuting = false;
    }
}
