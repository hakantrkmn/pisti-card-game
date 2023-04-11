using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICardPanelController : MonoBehaviour
{
    public List<CardController> cards;
    private GameStates gameState;
    public int playIndex;
    public Transform placePos;

    private void OnEnable()
    {
        EventManager.EndTurn += EndTurn;
        EventManager.ChangeGameState += ChangeGameState;
        EventManager.SetAICards += SetAICard;
    }

    private void EndTurn()
    {
        Debug.Log("sdfd");
        if (playIndex == EventManager.GetPlayIndex())
        {
            Debug.Log("12321");
            PlayRightCard();
        }
    }

    private void Start()
    {
        placePos = EventManager.GetCardPlacePos();
    }

    private void ChangeGameState(GameStates state)
    {
        gameState = state;
        if (state == GameStates.Game)
        {
            if (playIndex == EventManager.GetPlayIndex())
            {
                PlayRightCard();
            }
        }
    }

    public void PlayRightCard()
    {
        var placedCards = EventManager.GetPlacedCards();

        foreach (var card in placedCards)
        {
            foreach (var aiCard in cards)
            {
                if (aiCard.card.number == card.card.number)
                {
                    PlayCard(aiCard);

                    return;
                }
            }
        }
    }

    public void PlayCard(CardController cardController)
    {
        cardController.transform.position = placePos.position;
        cardController.transform.parent = placePos;
        EventManager.CardPlaced(cardController, playIndex);
    }

    private void OnDisable()
    {
        EventManager.EndTurn -= EndTurn;
        EventManager.ChangeGameState -= ChangeGameState;
        EventManager.SetAICards -= SetAICard;
    }

    private void SetAICard(List<CardController> obj)
    {
        foreach (var card in obj)
        {
            cards.Add(card);
        }

        foreach (var card in cards)
        {
            card.transform.parent = transform;
        }
    }
}