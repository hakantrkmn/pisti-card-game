using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardController : MonoBehaviour , IPointerDownHandler  , IDragHandler , IEndDragHandler
{
    public Card card;
    public Image image;
    public TextMeshProUGUI number;
    public CardTypes cardType;
    public bool isClosed;
    public int playerID;
    public Transform placePos;
    public bool canDrag;
    public void SetCard(Card createdCard, string number)
    {
        card.image = createdCard.image;
        card.cardType = createdCard.cardType;
        card.number = number;
        
        image.sprite = card.image;
        this.number.text = number;
        cardType = card.cardType;
    }


    private void Start()
    {
        placePos = EventManager.GetCardPlacePos();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            transform.position = Input.mousePosition;

        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
        if ((transform.position-placePos.position).magnitude < 50)
        {
            transform.position = placePos.position;
            transform.parent = placePos;
            EventManager.CardPlaced(this, playerID);
        }
    }
}
