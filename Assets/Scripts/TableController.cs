using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TableController : MonoBehaviour
{
    public GameStates gameState;
    public Transform allCardsPoint;

    public GameObject cardPrefab;

    public List<Card> createdCards;
    public List<Card> placedCards;

    public Transform placePos;

    public int playIndex;

    public List<CardPanel> allPlayers;

    private void OnEnable()
    {
        EventManager.DeckDone += DeckDone;
        EventManager.ChangeGameState += ChangeGameState;
        EventManager.EndTurn += EndTurn;
        EventManager.GetPlacedCards += () => placedCards;
        EventManager.GetPlayIndex += () => playIndex;
        EventManager.CardPlaced += CardPlaced;
        EventManager.GetCardPlacePos += GetCardPlacePos;
    }

    private void DeckDone()
    {
        var firstPlayer = allPlayers[0];

        foreach (var player in allPlayers)
        {
            if (player.collectedCardAmount > firstPlayer.collectedCardAmount)
            {
                firstPlayer = player;
            }
        }

        firstPlayer.PlayerHasMostCard();

        if (!CheckIfGameOver())
        {
            CreateCards();
            Put3ClosedCard();
            DealCards();
        }
        else
        {
        }
    }

    public bool CheckIfGameOver()
    {
        foreach (var player in allPlayers)
        {
            if (player.point > 20)
            {
                player.PlayerWonTheGame();
                return true;
            }
            
        }

        return false;
    }

    private void ChangeGameState(GameStates obj)
    {
        gameState = obj;
    }

    private void EndTurn()
    {
        if (createdCards.Count == 0)
        {
            //EventManager.DeckDone();
        }
    }

    private void OnDisable()
    {
        EventManager.GetCardPlacePos -= GetCardPlacePos;
        EventManager.DeckDone -= DeckDone;
        EventManager.ChangeGameState -= ChangeGameState;
        EventManager.CardPlaced -= CardPlaced;
        EventManager.EndTurn -= EndTurn;
    }

    private void CardPlaced(Card card)
    {
        placedCards.Add(card);

        if (placedCards.Count == 2)
        {
            CheckForPisti(card);
        }
        else if (placedCards.Count > 2)
        {
            CheckForEquality();
        }

        if (playIndex == allPlayers.Count - 1)
        {
            if (allPlayers[0].cards.Count == 0 && createdCards.Count != 0)
            {
                DealCards();
            }
            else if (createdCards.Count == 0 && allPlayers[0].cards.Count == 0)
            {
                EventManager.DeckDone();
            }
        }

        playIndex++;

        if (playIndex > allPlayers.Count - 1)
        {
            playIndex = 0;
        }


        EventManager.EndTurn();
    }

    public void SetPlayersIndex()
    {
        allPlayers.RemoveRange(EventManager.GetRoomData().aiAmount,allPlayers.Count-EventManager.GetRoomData().aiAmount);
        for (int i = 0; i < EventManager.GetRoomData().aiAmount; i++)
        {
            allPlayers[i].transform.parent.gameObject.SetActive(true);
        }
        for (int i = 0; i < allPlayers.Count; i++)
        {
            allPlayers[i].playIndex = i;
        }
    }

    public void ClearCards()
    {
        foreach (var card in placedCards)
        {
            Destroy(card.gameObject);
        }

        placedCards.Clear();
    }

    public void CheckForEquality()
    {
        if (placedCards[placedCards.Count - 1].value == placedCards[placedCards.Count - 2].value &&
            !placedCards[placedCards.Count - 2].isClosed)
        {
            EventManager.CollectCards(placedCards);
            ClearCards();
        }
    }

    public bool CheckForPisti(Card card)
    {
        if (placedCards[0].value == card.value && !placedCards[0].isClosed)
        {
            EventManager.Pisti();
            ClearCards();

            return true;
        }
        else
        {
            return false;
        }
    }

    private Transform GetCardPlacePos()
    {
        return placePos;
    }

    void Start()
    {
        SetPlayersIndex();
        CreateCards();
        Put3ClosedCard();
        DealCards();

        EventManager.ChangeGameState(GameStates.Game);
    }

    public void CreateCards()
    {
        var cardData = EventManager.GetCardData();

        for (int i = 0; i < cardData.cards.Count; i++)
        {
            for (int j = 1; j < 11; j++)
            {
                var card = Instantiate(cardPrefab, Vector3.zero, quaternion.identity, allCardsPoint)
                    .GetComponent<Card>();
                card.transform.localPosition = Vector3.zero;
                cardData.cards[i].value = j.ToString();
                card.SetCard(cardData.cards[i]);
                createdCards.Add(card);
            }
        }

        for (int i = 0; i < cardData.specialCards.Count; i++)
        {
            var card = Instantiate(cardPrefab, Vector3.zero, quaternion.identity, allCardsPoint).GetComponent<Card>();
            card.transform.localPosition = Vector3.zero;
            card.SetCard(cardData.specialCards[i]);
            createdCards.Add(card);
        }

        ShuffleList();
    }

    public void ShuffleList()
    {
        for (int i = 0; i < createdCards.Count; i++)
        {
            Card temp = createdCards[i];
            int randomIndex = Random.Range(i, createdCards.Count);
            createdCards[i] = createdCards[randomIndex];
            createdCards[randomIndex] = temp;
        }
    }

    public void Put3ClosedCard()
    {
        for (int i = 0; i < 3; i++)
        {
            createdCards[i].transform.position = placePos.position;
            createdCards[i].transform.parent = placePos;
            createdCards[i].CloseCard();
            placedCards.Add(createdCards[i]);
            createdCards.Remove(createdCards[i]);
        }

        createdCards[0].transform.position = placePos.position;
        createdCards[0].transform.parent = placePos;
        createdCards[0].ShowCard();
        placedCards.Add(createdCards[0]);
        createdCards.Remove(createdCards[0]);
    }

    public void DealCards()
    {
        for (int i = 0; i < allPlayers.Count; i++)
        {
            List<Card> cards = new List<Card>();
            for (int j = createdCards.Count - 1; j > createdCards.Count - 5; j--)
            {
                cards.Add(createdCards[j]);
            }

            EventManager.SetPlayerCards(cards, i);
            foreach (var card in cards)
            {
                createdCards.Remove(card);
            }

            cards.Clear();
        }
    }
}