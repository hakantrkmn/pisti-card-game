using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class BaseCard : MonoBehaviour,IDraggable
{
    public CardTypes cardType;
    public Sprite image;
    public string value;

    public Vector3 firstPos;
    public Sprite closedCardSprite;
    public TextMeshProUGUI valueText;
    public Image imageIMG;
    public bool isClosed;
    public Transform placePos;
    public bool canDrag;
    
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
