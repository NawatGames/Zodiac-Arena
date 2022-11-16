using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave : MonoBehaviour
{
    [SerializeField] private float targetSize;
    [SerializeField] private float growthSpeed;

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < targetSize) // O tamanho pode acabar um pouco maior que o targetSize
        {
            transform.localScale += new Vector3(growthSpeed * Time.deltaTime, growthSpeed * Time.deltaTime, 0);
        }
        else
            Destroy(gameObject);
    }
}
