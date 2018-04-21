using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStats : MonoBehaviour
{
    private static BossStats _instance;

    public static BossStats Instance
    {
        get { return _instance; }
    }
    
    public float HP;
    public float MaxHP;
    public float ManaChargeRate;


    public float Mana;
    public float MaxMana;
    public float HPChargeRate;


    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    void Update()
    {
        Mana += ManaChargeRate * Time.deltaTime;

        HP += HPChargeRate * Time.deltaTime;

        if (Mana > MaxMana)
            Mana = MaxMana;

        if (HP > MaxHP)
            HP = MaxHP;
    }
}
