using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerPanelManager : MonoBehaviour
{
    public TextMeshProUGUI usernameText;

    public TextMeshProUGUI moneyText;

    public TextMeshProUGUI scoreText;

    private int score;
    private void Start()
    {
        var playerData = EventManager.GetPlayerData();
        var roomData = EventManager.GetRoomData();
        usernameText.text = playerData.username;
        moneyText.text = roomData.bet.ToString();
        scoreText.text = "0";
    }

    public void UpdateScore(int increase)
    {
        score = increase;
        scoreText.text = score.ToString();

    }
}
