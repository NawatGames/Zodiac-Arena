using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balança : MonoBehaviour
{
    private Animator _animator;
    private EspadasDoJulgamento _espadasDoJulgamento;
    private int estado_balanca;
    void Start()
    {
        estado_balanca = Random.Range(0, 3);
        _espadasDoJulgamento = gameObject.GetComponent<EspadasDoJulgamento>();
        _animator = gameObject.GetComponent<Animator>();
        _animator.SetInteger("estado_balanca", estado_balanca);
        Debug.Log(estado_balanca);
        if (estado_balanca == 0)
        {
            Debug.Log("side");
            StartCoroutine(_espadasDoJulgamento.ThrowSideSword());
        }if (estado_balanca == 1)
        {
            StartCoroutine(_espadasDoJulgamento.ThrowRightSword());
            Debug.Log("right");
        }if (estado_balanca == 2)
        {
            StartCoroutine(_espadasDoJulgamento.ThrowLeftSword());
            Debug.Log("left");
        }if (estado_balanca == 3)
        {
            StartCoroutine(_espadasDoJulgamento.ThrowUpSword());
            Debug.Log("up");
        }
    }

    
}
