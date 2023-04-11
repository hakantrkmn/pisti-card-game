using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPanelController : MonoBehaviour
{
    public List<CardController> cards;
    private GameStates gameState;
    public int playIndex;
    public bool canPlay;
    private void OnEnable()
    {
        EventManager.ChangeGameState += ChangeGameState;
        EventManager.SetPlayerCards += SetPlayerCards;
    }
    
    

    private void ChangeGameState(GameStates state)
    {
        gameState = state;
        if (state==GameStates.Game)
        {
            if (playIndex==EventManager.GetPlayIndex())
            {
                canPlay = true;
                foreach (var card in cards)
                {
                    card.canDrag = true;
                }
            }
        }
    }

    private void OnDisable()
    {
        EventManager.ChangeGameState -= ChangeGameState;
        EventManager.SetPlayerCards -= SetPlayerCards;
    }

    private void SetPlayerCards(List<CardController> obj)
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