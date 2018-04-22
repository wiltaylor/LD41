using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardOwner
{
    Player,
    AI
}

public enum CardLocation
{
    InHand,
    InPlayBuff,
    InPlayBoss
}

public class CardController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerUpHandler
{
    public CardOwner Owner;
    public Color Selected;
    public Color Deselected;
    public CardData Data;
    public Image CardImage;
    public Text Name;
    public Text Cost;
    public Text CountDown;
    public bool HasCountDown;
    public float CountDownLeft;
    public CardTargetType CardLocation;
    public GameObject DisabledShield;

    private Image _image;

    void Start()
    {
        _image = GetComponent<Image>();

        SetSelect(false);

        UpdateCard();
    }


    public void SetSelect(bool selected)
    {
        _image.color = selected ? Selected : Deselected;
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        CardSystemController.Instance.ClickedOnCard(this);
    }

    public void UpdateCard()
    {
        if (Name != null)
            Name.text = Data.Name;

        if (CardImage != null)
            CardImage.sprite = Data.Picture;

        if (Cost != null)
            Cost.text = Data.Cost.ToString();

        if (CountDown != null && HasCountDown)
            CountDown.text = Mathf.RoundToInt(CountDownLeft).ToString();

        if (PlayerStats.Instance.Mana < Data.Cost && DisabledShield != null)
        {
            DisabledShield.SetActive(true);
        }else if (DisabledShield != null)
        {
            DisabledShield.SetActive(false);
        }
    }

    void Update()
    {
        UpdateCard();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CardSystemController.Instance.PreviewCard(Data);
    }

    public void Discard()
    {
        Destroy(gameObject);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right && CardLocation == CardTargetType.Hand)
            Discard();
    }
}

