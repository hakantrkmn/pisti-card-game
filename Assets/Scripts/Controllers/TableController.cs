using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TableController : MonoBehaviour
{
    GameStates gameState;
    public Transform allCardsPoint;
    public Transform placePos;


    public List<Card> createdCards;
    public List<Card> placedCards;
    public TextMeshProUGUI cardAmountText;

    int playIndex;
    
    public List<CardPanel> allPlayers;

    #region event subscribe

    

    private void OnEnable()
    {
        EventManager.DeckDone += DeckDone;
        EventManager.ChangeGameState += ChangeGameState;
        EventManager.CardPlaced += CardPlaced;
        EventManager.GetCardPlacePos += GetCardPlacePos;
        
        EventManager.GetPlacedCards += () => placedCards;
        EventManager.GetPlayIndex += () => playIndex;
    }
    
    private void OnDisable()
    {
        EventManager.GetCardPlacePos -= GetCardPlacePos;
        EventManager.DeckDone -= DeckDone;
        EventManager.ChangeGameState -= ChangeGameState;
        EventManager.CardPlaced -= CardPlaced;
    }
    #endregion


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
            Put4ClosedCard();
            DealCards();
        }
        
    }

    bool CheckIfGameOver()
    {
        foreach (var player in allPlayers)
        {
            if (player.point > EventManager.GetRoomData().maxPoint)
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


  
    private void CardPlaced(Card card)
    {
        placedCards.Add(card);

        if (placedCards.Count == 2) // 2 kart varsa pişti mi diye kontrol et
        {
            CheckForPisti(card);
        }
        else if (placedCards.Count > 2) // 2den fazlaysa son kartla aynı değere mi sahip kontrol et
        {
            CheckForEquality();
        }

        if (playIndex == allPlayers.Count - 1) // play index sona geldiyse sıfırla
        {
            if (allPlayers[0].cards.Count == 0 && createdCards.Count != 0) // son oyuncu oynadıysa ve kart kalmışsa kart dağıt
            {
                DealCards();
            }
            else if (createdCards.Count == 0 && allPlayers[0].cards.Count == 0) // son oyuncu oynamışsa ve 52 kart bitmişse kart dağıt
            {
                EventManager.DeckDone();
            }
        }

        playIndex++;

        if (playIndex > allPlayers.Count - 1)
        {
            playIndex = 0;
        }


        EventManager.EndTurn(); // sırayı sonraki oyuncuya geçir
    }

    void SetPlayersIndex() // her oyuncuya index atayıp bu indexe göre oyun oynuyoruz
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

    void ClearCards() 
    {
        placedCards.Clear();
    }

    void CheckForEquality() // son iki kart eşitmi diye kontrol etme
    {
        if (placedCards[placedCards.Count - 1].value == placedCards[placedCards.Count - 2].value &&
            !placedCards[placedCards.Count - 2].isClosed)
        {
            EventManager.CollectCards(placedCards);
            ClearCards();
        }
    }

    bool CheckForPisti(Card card) // pişti kontrolu
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
        Put4ClosedCard();
        DealCards();

        EventManager.ChangeGameState(GameStates.Game);
    }
    void CreateCards()// 52 tane kart oluştur
    {
        var cardData = EventManager.GetCardData();

        for (int i = 0; i < cardData.cards.Count; i++)
        {
            for (int j = 1; j < 11; j++)
            {
                var cardPrefab = EventManager.GetCardData().cardPrefab;
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
            var cardPrefab = EventManager.GetCardData().cardPrefab;
            var card = Instantiate(cardPrefab, Vector3.zero, quaternion.identity, allCardsPoint).GetComponent<Card>();
            card.transform.localPosition = Vector3.zero;
            card.SetCard(cardData.specialCards[i]);
            createdCards.Add(card);
        }

        ShuffleList();
    }

    void ShuffleList() // desteyi karıştır
    {
        for (int i = 0; i < createdCards.Count; i++)
        {
            Card temp = createdCards[i];
            int randomIndex = Random.Range(i, createdCards.Count);
            createdCards[i] = createdCards[randomIndex];
            createdCards[randomIndex] = temp;
        }
    }
    void Put4ClosedCard() // 3 kartı kapalı birini açık koy
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

    void DealCards() // kartları dağıt
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

        cardAmountText.text = createdCards.Count.ToString();
    }
}