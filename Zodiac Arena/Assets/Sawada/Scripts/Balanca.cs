using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balanca : MonoBehaviour
{
    private Animator _animator;
    private EspadasDoJulgamento _espadasDoJulgamento;
    private int estado_balanca;
    void Start()
    {
        StartAttack();
    }

    public void StartAttack()
    {
        estado_balanca = Random.Range(0, 3);
        _espadasDoJulgamento = gameObject.GetComponent<EspadasDoJulgamento>();
        _animator = gameObject.GetComponent<Animator>();
        _animator.SetInteger("estado_balanca", estado_balanca);
        _espadasDoJulgamento.isStartDelay = false;
        if (estado_balanca == 0)
        {
            Debug.Log("side");
            StartCoroutine(_espadasDoJulgamento.ThrowSideSword());
            gameObject.transform.Rotate(0,0,180);
        }if (estado_balanca == 1)
        {
            StartCoroutine(_espadasDoJulgamento.ThrowRightSword());
            // StartCoroutine(_espadasDoJulgamento.ThrowSideSword());
            Debug.Log("right");
            gameObject.transform.Rotate(0,0,-90);
        }if (estado_balanca == 2)
        {
            StartCoroutine(_espadasDoJulgamento.ThrowLeftSword());
            // StartCoroutine(_espadasDoJulgamento.ThrowSideSword());
            Debug.Log("left");
            gameObject.transform.Rotate(0,0,90);
        }if (estado_balanca == 3)
        {
            StartCoroutine(_espadasDoJulgamento.ThrowUpSword());
            // StartCoroutine(_espadasDoJulgamento.ThrowSideSword());
            Debug.Log("up");
        }
    }
    
}
