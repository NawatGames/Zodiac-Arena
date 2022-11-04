using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LionManager : MonoBehaviour
{
    public GameObject spikePrefabRight;
    public Transform spikePositionRight;
    
    public GameObject spikePrefabLeft;
    public Transform spikePositionLeft;
    
    public GameObject spikePrefabDown;
    public Transform spikePositionDown;

    private GameObject randomKey;

    public float timeinseconds;
    
    private bool isCoroutineExecuting;
    private Dictionary<GameObject, Transform> dict;
    void Start()
    {
        dict = new Dictionary<GameObject, Transform>();
        
        dict.Add(spikePrefabRight,spikePositionRight);
        dict.Add(spikePrefabDown,spikePositionDown);
        dict.Add(spikePrefabLeft,spikePositionLeft);

        Debug.Log(randomKey);
        
    }

    public void Update()
    {
        StartCoroutine(ExecuteAfterTime(Random.Range(0,timeinseconds), dict));
    }

    IEnumerator ExecuteAfterTime(float time, Dictionary<GameObject,Transform> dict)
    {
        if (isCoroutineExecuting)
            yield break;
        
        isCoroutineExecuting = true;
        
        yield return new WaitForSeconds(time);
        randomKey = dict.ElementAt(Random.Range(0, dict.Count)).Key;
        Instantiate(randomKey, dict[randomKey].position, dict[randomKey].rotation);

        isCoroutineExecuting = false;
    }


   
}
