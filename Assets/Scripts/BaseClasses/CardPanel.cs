using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class CardPanel : MonoBehaviour // cardpaneli base classı
{
    public List<Card> cards;
    public int collectedCardAmount;

    GameStates gameState;
    public int playIndex;
    public int point;
    
    
    public virtual void OnEnable()
    {
        EventManager.CollectCards += CollectCards;
        EventManager.ChangeGameState += ChangeGameState;
        EventManager.Pisti += Pisti;
    }


    public virtual void PlayerWonTheGame(){}
    public virtual void PlayerLostTheGame() { }
    public virtual void PlayerHasMostCard()
    {
        point += 10;
    }

    public virtual void OnDisable()
    {
        EventManager.ChangeGameState -= ChangeGameState;
        EventManager.CollectCards -= CollectCards;
        EventManager.Pisti -= Pisti;
    }
    
    public virtual void ChangeGameState(GameStates state)
    {
        gameState = state;
        
    }
    public virtual void CollectCards(List<Card> placedCards)
    {
        if (playIndex == EventManager.GetPlayIndex())
        {
            foreach (var card in placedCards)
            {
                if (card.cardType == CardTypes.Clubs && card.value == "2")
                {
                    point += 2;
                }
                else if (card.cardType == CardTypes.Diamonds && card.value == "10")
                {
                    point += 10;
                }
                else if (card.value == "1")
                {
                    point += 1;
                }
                else if (card.value == "j")
                {
                    point += 1;
                }

                card.transform.DOMove(transform.position, .5f).OnComplete((() =>
                {
                    Destroy(card.gameObject);
                }));
            }
            collectedCardAmount += placedCards.Count;
            
        }
    }
    private void Pisti()
    {
        if (playIndex == EventManager.GetPlayIndex())
        {
            point += 10;
        }
    }
    
  
}
