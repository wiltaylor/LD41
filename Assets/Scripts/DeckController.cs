using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DeckController : MonoBehaviour
{
    private static DeckController _instance;

    public static DeckController Instance
    {
        get { return _instance; }
    }
    
    public float CoolDown = 10f;
    public DeckData Deck;
    public Text CountDownText;

    private float _currentCoolDown;

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);

            return;
        }

        _instance = this;

        _currentCoolDown = CoolDown;
    }

    public void ForceDraw()
    {
        if (HandController.Instance.CanAddCards())
        {
            var card = DrawCard();
            HandController.Instance.AddCard(card);
        }
    }
    
	void Update ()
	{
	    if (PlayerStats.Instance.HP <= 0)
	        return;


	    CountDownText.text = Mathf.RoundToInt(_currentCoolDown).ToString();


        if (_currentCoolDown <= 0f)
	    {
	        _currentCoolDown = CoolDown;

	        if (HandController.Instance.CanAddCards())
	        {
	            var card = DrawCard();
                HandController.Instance.AddCard(card);
            }

	        return;
	    }

	    _currentCoolDown -= Time.deltaTime;
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
}
