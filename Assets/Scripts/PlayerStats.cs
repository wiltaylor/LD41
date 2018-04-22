using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    private static PlayerStats _instance;

    public static PlayerStats Instance
    {
        get { return _instance; }
    }


    public float Mana;
    public float MaxMana;
    public float ManaChargeRate;

    public float HP;
    public float MaxHP;
    public float HPChargeRate;

    public float HPCap = 28;
    public float ManaCap = 28;

    public int KillCount;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

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
        if (HP <= 0)
            return;

        if (MaxHP > HPCap)
            MaxHP = HPCap;

        if (MaxMana > ManaCap)
            MaxMana = ManaCap;

        Mana += ManaChargeRate * Time.deltaTime;
        HP += HPChargeRate * Time.deltaTime;

        if (HP > MaxHP)
            HP = MaxHP;

        if (Mana > MaxMana)
            Mana = MaxMana;

    }


}
