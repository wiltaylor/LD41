using System;
using UnityEngine;
using UnityEngine.UI;


public enum SelectionState
{
    Ready,
    CardSelected,
}

public enum CardTargetType
{
    Buff,
    BossCard,
    Hand
}

public class CardSystemController : MonoBehaviour
{
    private static CardSystemController _instance;


    public Text PreviewName;
    public Text PreviewCost;
    public Text PreviewType;
    public Text PreviewText;
    public Image PreviewImage;

    public CardController CurrentTarget;
    public CardController CurrentSelection;

    private SelectionState _currentState = SelectionState.Ready;
    private CardTargetType _currentTargetTypeType;

    public static CardSystemController Instance
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

    public void ClickedOnCard(CardController card)
    {
        Debug.Log("Card clicked!!!!");


        if (PlayerStats.Instance.HP <= 0)
            return;


        if (_currentState == SelectionState.CardSelected && card == CurrentSelection)
        {
            card.SetSelect(false);
            CurrentSelection = null;
            _currentState = SelectionState.Ready;
            return;

        }


        if (_currentState == SelectionState.CardSelected && card.CardLocation == _currentTargetTypeType)
        {
            CurrentTarget = card;
            CurrentSelection.Data.OnActivation.Invoke();
            CurrentSelection.Discard();
            PayCardCost(CurrentSelection.Data);

            CurrentSelection = null;
            CurrentTarget = null;

            _currentState = SelectionState.Ready;
            return;
        }

        if (_currentState == SelectionState.Ready)
        {
            if (!CanCast(card.Data))
                return;

            card.Data.OnCast.Invoke();

            switch (card.Data.CastAction)
            {
                case ActionOnCast.Discard:
                    card.Discard();
                    PayCardCost(card.Data);
                    break;
                case ActionOnCast.MoveToBuffs:
                    CardStack.BuffInstance.AddCard(card.Data);
                    PayCardCost(card.Data);
                    card.Discard();
                    break;
                case ActionOnCast.Target:
                    card.SetSelect(true);
                    _currentState = SelectionState.CardSelected;
                    _currentTargetTypeType = card.Data.TargetType;
                    CurrentSelection = card;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }

    public void ClearSelection()
    {
        CurrentSelection = null;
        CurrentTarget = null;
        _currentState = SelectionState.Ready;
    }

    public bool CanCast(CardData data)
    {
        return PlayerStats.Instance.Mana >= data.Cost;
    }

    public void PayCardCost(CardData data)
    {
        PlayerStats.Instance.Mana -= data.Cost;
    }

    public void PreviewCard(CardData data)
    {
        PreviewName.text = data.Name;
        PreviewCost.text = data.Cost.ToString();
        PreviewType.text = data.Type;
        PreviewText.text = data.Description;
        PreviewImage.sprite = data.Picture;
        
    }


}
