using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class AICardPanelController : CardPanel
{
    public AIPanelManager aiPanelManager;
    public Transform placePos;
    public override void OnEnable()
    {
        base.OnEnable();
        EventManager.EndTurn += EndTurn;
        EventManager.SetPlayerCards += SetAICard;
    }
    public override void OnDisable()
    {
        base.OnDisable();
        EventManager.EndTurn -= EndTurn;
        EventManager.SetPlayerCards -= SetAICard;
    }

    public override void PlayerWonTheGame()
    {
        base.PlayerWonTheGame();
        EventManager.SetLose();
    }
    

    private void EndTurn()
    {
        if (playIndex == EventManager.GetPlayIndex())
        {
            FindRightCard();
        }
    }

   
    private void Start()
    {
        placePos = EventManager.GetCardPlacePos();
    }

    public override void ChangeGameState(GameStates state)
    {
        base.ChangeGameState(state);
        
        if (state == GameStates.Game)
        {
            if (playIndex == EventManager.GetPlayIndex())
            {
                FindRightCard();
            }
        }
    }

    public override void CollectCards(List<Card> placedCards)
    {
        base.CollectCards(placedCards);
        aiPanelManager.UpdateScore(point);
    }

    public override void PlayerHasMostCard()
    {
        base.PlayerHasMostCard();
        aiPanelManager.UpdateScore(point);
    }
    void FindRightCard()
    {
        if (EventManager.GetPlacedCards().Count > 0)
        {
            var card = EventManager.GetPlacedCards().Last();
            foreach (var aiCard in cards)
            {
                if (aiCard.value == card.value && !card.isClosed)
                {
                    cards.Remove(aiCard);
                    PlayCard(aiCard);
                    return;
                }
            }

            var tempCard = cards[Random.Range(0, cards.Count)];
            cards.Remove(tempCard);
            PlayCard(tempCard);
        }
        else
        {
            var tempCard = cards[Random.Range(0, cards.Count)];
            cards.Remove(tempCard);
            PlayCard(tempCard);
        }
    }

    void PlayCard(Card card)
    {
        Sequence aiPlay = DOTween.Sequence();
        aiPlay.AppendCallback((() => card.ShowCard()));

        aiPlay.Append(card.transform.DOMove(placePos.position, .5f));
        aiPlay.AppendCallback((() =>
        {
            card.transform.parent = placePos;
            EventManager.CardPlaced(card);
        }));
    }

   

    private void SetAICard(List<Card> playerCards,int index)
    {
        if (index==playIndex)
        {
            foreach (var card in playerCards)
            {
                cards.Add(card);
                card.transform.parent = transform;

            }
        }
        
    }
}