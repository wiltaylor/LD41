using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destructable : MonoBehaviour
{
    public float HP;
    public UnityEvent OnDeath;
    private bool doneDeath = false;

    private float _startHp;

    void Start()
    {
        _startHp = HP;
    }

    public void DoDamage(float ammount)
    {
        HP -= ammount;
    }

    void Update()
    {
        if (doneDeath)
            return;

        if (HP < 0)
        {
            OnDeath.Invoke();
            doneDeath = true;
        }
    }

    void OnEnable()
    {
        doneDeath = false;
        HP = _startHp;
    }
}
