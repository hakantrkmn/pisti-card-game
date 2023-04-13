using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Card : BaseCard
{
    public override void OnBeginDrag(PointerEventData eventData)
    {
        firstPos = transform.position;
    }
    public override void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            
            transform.position = Input.mousePosition;

        }
    }

    public override void OnEndDrag(PointerEventData eventData)
    {
        
        if ((transform.position-placePos.position).magnitude < 50)
        {
            transform.parent.GetComponent<CardPanelController>().cards.Remove(this);
            transform.position = placePos.position;
            transform.parent = placePos;
            EventManager.CardPlaced(this);
        }
        else
        {
            transform.DOMove(firstPos, .5f);
        }
    }
}
