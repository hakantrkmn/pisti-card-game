using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseCard : MonoBehaviour,IDraggable
{
    [FoldoutGroup("Card")]
    public CardTypes cardType;
    [FoldoutGroup("Card")]
    public Sprite image;
    [FoldoutGroup("Card")]
    public string value;
    [FoldoutGroup("Card")]
    public Sprite closedCardSprite;
    [FoldoutGroup("Card")]
    public TextMeshProUGUI valueText;
    [FoldoutGroup("Card")]
    public Image imageIMG;
    [HideInInspector]
    public bool isClosed;
    protected Transform placePos;
    [HideInInspector]
    public bool canDrag;
    protected Vector3 firstPos;

    public void SetCard(CardStats createdCard)
    {
        image = createdCard.image;
        cardType = createdCard.cardType;
        value = createdCard.value;
        CloseCard();
    }

    public void ShowCard()
    {
        valueText.text = value;
        imageIMG.sprite = image;
        isClosed = false;
    }
    private void Start()
    {
        placePos = EventManager.GetCardPlacePos();
    }

    public void CloseCard()
    {
        imageIMG.sprite = closedCardSprite;
        valueText.text = "";
        isClosed = true;
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        
       
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        
        
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        
    }
}
