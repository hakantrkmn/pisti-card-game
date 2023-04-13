using System.Collections;
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

    public AIProfiles aiProfiles;
    //---------------------------------------------------------------------------------
    private void OnEnable()
    {
        EventManager.GetCardData += GetCardData;
        EventManager.GetPlayerData += GetPlayerData;
        EventManager.GetRoomData += GetRoomData;
        EventManager.GetAiProfiles += () => aiProfiles;

        EventManager.NewGameClicked += NewGameClicked;
        EventManager.BackToLobbyClicked += BackToLobbyClicked;

        EventManager.StartLevel += LevelStarted;
        EventManager.SetWin += SetWinGame;
        EventManager.SetLose += SetLoseGame;
        EventManager.IsGameCompleted = () => isGameCompleted;
        EventManager.IsGameStarted = () => isGameStarted;
    }

    private void BackToLobbyClicked()
    {
        SceneManager.LoadScene(0);
    }

    private void NewGameClicked()
    {
        SceneManager.LoadScene(1);
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
        EventManager.BackToLobbyClicked -= BackToLobbyClicked;
        EventManager.NewGameClicked -= NewGameClicked;
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
      
    }


    //---------------------------------------------------------------------------------
    private void SetLoseGame()
    {
        
    }
}