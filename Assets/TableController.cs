using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TableController : MonoBehaviour
{
    public Transform allCardsPoint;

    public GameObject cardPrefab;

    public List<CardController> createdCards;
    public List<CardController> placedCards;

    public Transform placePos;

    public  int playIndex;

    private void OnEnable()
    {
        EventManager.GetPlacedCards += () => placedCards;
        EventManager.GetPlayIndex += () => playIndex;
        EventManager.CardPlaced += CardPlaced;
        EventManager.GetCardPlacePos += GetCardPlacePos;
    }

    private void OnDisable()
    {
        EventManager.CardPlaced -= CardPlaced;

    }

    private void CardPlaced(CardController cardController, int playerID)
    {
        placedCards.Add(cardController);

        if (placedCards.Count==2)
        {
            CheckForPisti(cardController);
        }

        playIndex++;

        EventManager.EndTurn();

    }

    public bool CheckForPisti(CardController cardController)
    {
        if (placedCards[0].card.number == cardController.card.number)
        {
            Debug.Log("pişti");
            return true;
        }
        else
        {
            Debug.Log("pişti değil");
            return false;
        }
    }

    private Transform GetCardPlacePos()
    {
        return placePos;
    }

    void Start()
    {
        CreateCards();
       // Put3ClosedCard();
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
                 var card = Instantiate(cardPrefab, Vector3.zero, quaternion.identity, allCardsPoint).GetComponent<CardController>();
                 card.transform.localPosition = Vector3.zero;
                 card.SetCard(cardData.cards[i],j.ToString());
                 createdCards.Add(card);
             }
             
         }
         
        for (int i = 0; i < cardData.specialCards.Count; i++)
        {
            var card = Instantiate(cardPrefab, Vector3.zero, quaternion.identity, allCardsPoint).GetComponent<CardController>();
            card.transform.localPosition = Vector3.zero;
            card.SetCard(cardData.specialCards[i], cardData.specialCards[i].number);
            createdCards.Add(card);

        }
        
        ShuffleList();
    }

    public void ShuffleList()
    {
        for (int i = 0; i < createdCards.Count; i++) {
            CardController temp = createdCards[i];
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
            placedCards.Add(createdCards[i]);
            createdCards.Remove(createdCards[i]);
        }
    }

    public void DealCards()
    {
        List<CardController> playerCards = new List<CardController>();
        List<CardController> aiCards = new List<CardController>();

        for (int i = createdCards.Count-1; i > createdCards.Count-9; i--)
        {
            if (i%2==0)
            {
                playerCards.Add(createdCards[i]);

            }
            else
            {
                aiCards.Add(createdCards[i]);
            }
        }

        EventManager.SetPlayerCards(playerCards);
        EventManager.SetAICards(aiCards);

    }
}