using UnityEngine;

public class HandController : MonoBehaviour
{
    public GameObject CardPrefab;
    public int MaxCards = 3;
    public int StartingCards = 3;

    private static HandController _instance;

    public static HandController Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    void Start()
    {
        for(int i = 0; i < StartingCards; i++)
            DeckController.Instance.ForceDraw();
    }

    public bool CanAddCards()
    {
        return transform.childCount < MaxCards;
    }

    public void AddCard(CardData data)
    {
        if (!CanAddCards())
            return;

        var newCard = Instantiate(CardPrefab);
        newCard.transform.SetParent(transform);
        newCard.SetActive(true);

        var controller = newCard.GetComponent<CardController>();
        controller.Data = data;
        controller.UpdateCard();
        controller.CardLocation = CardTargetType.Hand;
    }

    public void UnselectAll()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var controller = transform.GetChild(i).GetComponent<CardController>();
            controller.SetSelect(false);
        }
    }
}
