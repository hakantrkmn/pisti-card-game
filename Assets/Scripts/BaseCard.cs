using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCard : MonoBehaviour
{
    public Card card;
    
    public bool isClosed;
    protected Transform placePos;
    public bool canDrag;
    
    public void SetCard(Card createdCard)
    {
        card.image = createdCard.image;
        card.cardType = createdCard.cardType;
        
    }
    private void Start()
    {
        placePos = EventManager.GetCardPlacePos();
    }
}
