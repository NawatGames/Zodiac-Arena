using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class LionManager : MonoBehaviour
{
    [Header("Atributos do Dicionário")]
    public GameObject spikePrefabRight;
    public Transform spikePositionRight;
    
    public GameObject spikePrefabLeft;
    public Transform spikePositionLeft;
    
    public GameObject spikePrefabDown;
    public Transform spikePositionDown;

    private GameObject randomKey;

    [Header("Tempo Máximo")]
    public float timeinseconds;
    
    private bool isCoroutineExecuting;
    private Dictionary<GameObject, Transform> dict;
    void Start()
    {
        // Criação de dicionário
        dict = new Dictionary<GameObject, Transform>();
        
        // Adição de elemento no dicionário
        dict.Add(spikePrefabRight,spikePositionRight);
        dict.Add(spikePrefabDown,spikePositionDown);
        dict.Add(spikePrefabLeft,spikePositionLeft);
    }

    public void Update()
    {
        StartCoroutine(ExecuteAfterTime(Random.Range(0,timeinseconds), dict));
    }

    // Após um tempo randômico gera um game object (prefab)
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

    // Quando o Player entrar nessa área, o leão mata
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.name == "Player")
    //    {
    //        Destroy(other.gameObject);
    //    }
    //}

   
}
