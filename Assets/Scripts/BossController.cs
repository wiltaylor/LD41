using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour
{

    private static BossController _instance;

    public static BossController Instance
    {
        get { return _instance; }
    }

    public DeckData Deck;
    public float DrawTimeOut;
    public BossData CurrentBoss;
    public Text NameText;
    public Image BossImage;
    public Color DialogueColour;

    private CardData currentCard;
    private float _timeout;
    private BossStats _bossStats;
     

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }


	void Start ()
	{
	    _bossStats = GetComponent<BossStats>();

	}
	
	void Update ()
	{
	    if (PlayerStats.Instance.HP <= 0)
	        return;

	    if (CurrentBoss == null)
	    {
            NameText.gameObject.SetActive(false);
            BossImage.gameObject.SetActive(false);
	        return;
        }

	    NameText.gameObject.SetActive(true);
	    BossImage.gameObject.SetActive(true);


	    if (_bossStats.HP <= 0) //Do death stuff here
	    {
	        TextMessageSystem.Instance.Clear();

            foreach (var item in CurrentBoss.DeathText)
                TextMessageSystem.Instance.AddMessage(item.Text, item.Time, DialogueColour);

	        CurrentBoss = null;
	        return;
	    }


	    _timeout -= Time.deltaTime;


	    if (_timeout < 0f)
	        _timeout = 0f;

	    if (currentCard == null && _timeout <= 0f)
	    {
	        currentCard = DrawCard();
	    }

	    if (currentCard != null && _bossStats.Mana >= currentCard.Cost)
	    {
	        _bossStats.Mana -= currentCard.Cost;

            switch (currentCard.CastAction)
	        {
	            case ActionOnCast.Discard:
	                currentCard.OnCast.Invoke();
	                currentCard = null;
	                break;
	            case ActionOnCast.MoveToBuffs:
                    CardStack.BossInstance.AddCard(currentCard);
	                currentCard = null;
	                break;
	            default:
	                throw new ArgumentOutOfRangeException();
	        }

	        
	        _timeout = DrawTimeOut;
	    }
	}


    CardData DrawCard()
    {
        var maxrange = 0f;

        foreach (var item in Deck.Items)
            maxrange += item.Chance;

        var selection = Random.Range(0, maxrange);

        foreach (var item in Deck.Items)
        {
            selection -= item.Chance;

            if (selection <= 0f)
                return item.Card;
        }

        return Deck.Items.Last().Card;

    }

    public void SetNewBoss(BossData boss)
    {
        CurrentBoss = boss;

        _bossStats.MaxHP = boss.MaxHP;
        _bossStats.MaxMana = boss.MaxMana;
        _bossStats.HPChargeRate = boss.HPChargeRate;
        _bossStats.ManaChargeRate = boss.ManaChargeRate;

        _bossStats.HP = _bossStats.MaxHP;
        _bossStats.Mana = _bossStats.MaxMana;

        NameText.text = boss.Name;
        BossImage.sprite = boss.Picture;
        Deck = boss.Deck;
        DrawTimeOut = boss.CoolDown;
    }

    public void TakeDamage(float ammount)
    {
        _bossStats.HP -= ammount;
        TextMessageSystem.Instance.Clear();

        foreach (var item in CurrentBoss.OnHitText)
            TextMessageSystem.Instance.AddMessage(item.Text, item.Time, DialogueColour);
    }
}
