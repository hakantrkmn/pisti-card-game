﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameData gameData;
    private bool _isProgress;
    private bool isGameCompleted;
    private bool isGameStarted;
    private Scene thisScene;
    public PlayerData playerData;
    public RoomData roomData;
    public CardData cardData;

    //---------------------------------------------------------------------------------
    private void OnEnable()
    {
        EventManager.GetCardData += GetCardData;
        EventManager.GetPlayerData += GetPlayerData;
        EventManager.GetRoomData += GetRoomData;

        EventManager.StartLevel += LevelStarted;
        EventManager.SetWin += SetWinGame;
        EventManager.SetLose += SetLoseGame;
        EventManager.IsGameCompleted = () => isGameCompleted;
        EventManager.IsGameStarted = () => isGameStarted;
    }

    private CardData GetCardData()
    {
        return cardData;
    }

    private RoomData GetRoomData()
    {
        return roomData;
    }

    private PlayerData GetPlayerData()
    {
        return playerData;
    }

    private void OnDisable()
    {
        EventManager.StartLevel -= LevelStarted;
        EventManager.SetWin -= SetWinGame;
        EventManager.SetLose -= SetLoseGame;
    }


    //---------------------------------------------------------------------------------
   


    //---------------------------------------------------------------------------------
    private void LevelStarted()
    {
        isGameStarted = true;
        EventManager.OnGameStarted?.Invoke();

        thisScene = SceneManager.GetActiveScene();
    }


    //---------------------------------------------------------------------------------
    private void SetWinGame()
    {
        if (_isProgress == true)
            return;

        isGameCompleted = true;
        EventManager.OnGameCompleted?.Invoke();


        gameData.fakeLevelIndex++;

        if (gameData.fakeLevelIndex % gameData.levelLoopValue == 0)
            gameData.realLevelIndex = 0;
        else
            gameData.realLevelIndex = gameData.fakeLevelIndex % gameData.levelLoopValue;

        _isProgress = true;

        SaveManager.SaveGameData(gameData);
    }


    //---------------------------------------------------------------------------------
    private void SetLoseGame()
    {
        isGameCompleted = true;
        EventManager.OnGameCompleted?.Invoke();
    }
}