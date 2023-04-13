using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserCardPanelController : CardPanel
{
    public PlayerPanelManager panelManager;
    public bool canPlay;
    public override void OnEnable()
    {
        base.OnEnable();
        EventManager.SetLose+= SetLose;
        EventManager.SetWin += SetWin;
        EventManager.CardPlaced += CardPlaced;
        EventManager.EndTurn += EndTurn;
        EventManager.SetPlayerCards += SetPlayerCards;
    }
    public override void PlayerWonTheGame()
    {
        base.PlayerWonTheGame();
        EventManager.SetWin();
    }
    public override void OnDisable()
    {
        base.OnDisable();
        EventManager.SetWin -= SetWin;
        EventManager.SetLose -= SetLose;
        EventManager.CardPlaced -= CardPlaced;
        EventManager.EndTurn -= EndTurn;
        EventManager.SetPlayerCards -= SetPlayerCards;
    }

    private void SetLose()
    {
        EventManager.GetPlayerData().money -= EventManager.GetRoomData().bet;
        EventManager.GetPlayerData().loseAmount++;
    }

    private void SetWin()
    {
        EventManager.GetPlayerData().money += EventManager.GetRoomData().bet;
        EventManager.GetPlayerData().winAmount++;

    }

    public override void PlayerHasMostCard()
    {
        base.PlayerHasMostCard();
        panelManager.UpdateScore(point);
    }
    private void CardPlaced(Card card)
    {
        if (cards.Contains(card))
        {
            cards.Remove(card);
        }
    }

    public override void CollectCards(List<Card> placedCards)
    {
        base.CollectCards(placedCards);
        panelManager.UpdateScore(point);
    }


    private void EndTurn()
    {
        if (playIndex == EventManager.GetPlayIndex())
        {
            PlayerCanPlay(true);
        }
        else
        {
            PlayerCanPlay(false);
        }
    }


    public override void ChangeGameState(GameStates state)
    {
        base.ChangeGameState(state);
        if (state == GameStates.Game)
        {
            if (playIndex == EventManager.GetPlayIndex())
            {
                PlayerCanPlay(true);
            }
        }
    }

    private void PlayerCanPlay(bool canPlay)
    {
        this.canPlay = canPlay;
        foreach (var card in cards)
        {
            card.canDrag = canPlay;
        }
    }


    private void SetPlayerCards(List<Card> playerCards,int index)
    {
        if (index==playIndex)
        {
            foreach (var card in playerCards)
            {
                cards.Add(card);
                card.ShowCard();
                card.transform.parent = transform;
            }
        }
        
        
    }
}