using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public float Mana;
    public float MaxMana;
    public float ManaChargeRate;

    public float HP;
    public float MaxHP;
    public float HPChargeRate;

    public UnityEvent OnDeath;

    private bool hasProcessedDeath = false;

    public void UpdateHP(float ammount)
    {
        HP += ammount;
    }

    public void UpdateMana(float ammount)
    {
        Mana += ammount;
    }

    void Update()
    {
        if (hasProcessedDeath)
            return;

        if (HP <= 0f)
        {
            OnDeath.Invoke();
            hasProcessedDeath = true;
            HP = 0;
            return;
        }

        Mana += ManaChargeRate * Time.deltaTime;
        HP += HPChargeRate * Time.deltaTime;

        if (HP > MaxHP)
            HP = MaxHP;

        if (Mana > MaxMana)
            Mana = MaxMana;

    }


}
