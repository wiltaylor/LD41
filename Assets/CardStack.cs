using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum StackType
{
    Boss,
    Buff
}

public class CardStack : MonoBehaviour
{
    private static CardStack _bossInstance;
    private static CardStack _buffInstance;

    public static CardStack BossInstance
    {
        get { return _bossInstance; }
    }

    public static CardStack BuffInstance
    {
        get { return _buffInstance; }
    }


    public GameObject CardPrefab;
    public StackType Type;

    private readonly List<CardController> _cardControllers = new List<CardController>();

    void Awake()
    {
        if ((Type == StackType.Boss && _bossInstance != null) || (Type == StackType.Buff && _buffInstance != null))
        {
            Destroy(gameObject);
            return;
        }

        if (Type == StackType.Buff)
            _buffInstance = this;

        if (Type == StackType.Boss)
            _bossInstance = this;
    }

    public void AddCard(CardData data)
    {
        var obj = Instantiate(CardPrefab);
        obj.transform.SetParent(transform);
        obj.SetActive(true);

        var controller = obj.GetComponent<CardController>();

        controller.Data = data;
        controller.UpdateCard();
        controller.CardLocation = Type == StackType.Boss ? CardTargetType.BossCard : CardTargetType.Buff;

        if (data.Timer > 0)
        {
            controller.CountDownLeft = data.Timer;
            controller.HasCountDown = true;
        }
        
        _cardControllers.Add(controller);


    }

    void Update()
    {
        _cardControllers.RemoveAll(c => c == null);

        foreach (var card in _cardControllers.Where(c => c.HasCountDown))
        {
            card.CountDownLeft -= Time.deltaTime;
            card.UpdateCard();
        }

        foreach (var card in _cardControllers.Where(c => c.HasCountDown && c.CountDownLeft <= 0))
        {
            card.Data.OnExpire.Invoke();
            card.Discard();
        }
    }
}
 