using System;
using System.Collections.Generic;
using UnityEngine;


public static class EventManager
{
 

    #region GameManagerEvents
    public static Action SetWin;
    public static Action SetLose;
   
    #endregion

  
    
   
    
    public static Action<int> RoomArrowClicked;
    public static Action CreateRoomClicked;
    public static Action NewGameClicked;
    public static Action BackToLobbyClicked;
    public static Action SideMenuClicked;

    public static Action CreateRoomExitClicked;
    public static Action ProfileButtonClicked;
    public static Action PlayButtonClicked;
    public static Action CreateTableClicked;

    
    public static Action<List<Card>,int> SetPlayerCards;
    public static Action<List<Card>> SetAICards;
    public static Action<Card> CardPlaced;
    public static Action<GameStates> ChangeGameState;
    public static Action EndTurn;
    public static Action Pisti;
    public static Action<List<Card>> CollectCards;

    public static Action DeckDone;

    public static Func<PlayerData> GetPlayerData;
    public static Func<RoomData> GetRoomData;
    public static Func<CardData> GetCardData;
    public static Func<AIProfiles> GetAiProfiles;

    public static Func<Transform> GetCardPlacePos;
    public static Func<List<Card>> GetPlacedCards;
    public static Func<int> GetFirstPlayerCardAmount;

    public static Func<int> GetPlayIndex;

}