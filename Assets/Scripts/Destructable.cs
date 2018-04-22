using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Destructable : MonoBehaviour
{
    public float HP;
    public UnityEvent OnDeath;
    private bool doneDeath = false;

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

    public void DestroyMe(float timeout)
    {
        Destroy(gameObject, timeout);
    }
}
