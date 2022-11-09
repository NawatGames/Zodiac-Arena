using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    public float timeinsecondsmirror;
    public GameObject mirror;
    
    private bool isCoroutineExecuting;
    public void Update()
    {
        StartCoroutine(ExecuteAfterTimeEnable(Random.Range(0,timeinsecondsmirror)));
    }

    // Após um tempo randômico ativa o espelho sem ativar a luz
    IEnumerator ExecuteAfterTimeEnable(float time)
    {
        if (isCoroutineExecuting)
            yield break;
        
        isCoroutineExecuting = true;
        
        yield return new WaitForSeconds(time);
        mirror.gameObject.SetActive(true);

        isCoroutineExecuting = false;
    }
}
