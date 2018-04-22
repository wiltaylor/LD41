using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Destructable : MonoBehaviour
{
    public float HP;
    public UnityEvent OnDeath;
    private bool doneDeath = false;
    public GameObject OnDeathPrefab;
    public GameObject CardPickUpPrefab;
    public bool IncrementKillCount;
    public LootTable LootTable;

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

            if (OnDeathPrefab != null)
            {
                var meat = PoolSystem.Instance.GetProjectile(OnDeathPrefab);

                meat.transform.position = transform.position;
                meat.SetActive(true);


                if (IncrementKillCount)
                    PlayerStats.Instance.KillCount++;

                if (LootTable != null)
                {
                    var card = GetCardFromLootTable();

                    if (card == null)
                        return;

                    var pickup = PoolSystem.Instance.GetProjectile(CardPickUpPrefab);
                    var controller = pickup.GetComponent<PickUpController>();
                    controller.Card = card;

                    pickup.transform.position = transform.position;
                    pickup.SetActive(true);
                }

            }
        }
    }

    void OnEnable()
    {
        doneDeath = false;
        HP = _startHp;
    }

    CardData GetCardFromLootTable()
    {
        var maxrange = 0f;

        foreach (var item in LootTable.Items)
            maxrange += item.Chance;

        var selection = Random.Range(0, maxrange);

        foreach (var item in LootTable.Items)
        {
            selection -= item.Chance;

            if (selection <= 0f)
                return item.Card;
        }

        return LootTable.Items.Last().Card;

    }
}
