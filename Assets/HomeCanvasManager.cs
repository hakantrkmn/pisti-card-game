using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HomeCanvasManager : MonoBehaviour
{
    public TextMeshProUGUI playerUsername;
    public TextMeshProUGUI playerMoney;

    public GameObject gamePanel;
    public GameObject topPanel;
    public GameObject createRoomPanel;
    public GameObject profilePanel;


    private void OnEnable()
    {
        EventManager.ProfileButtonClicked += ProfileButtonClicked;
        EventManager.CreateRoomExitClicked += CreateRoomExitClicked;
        EventManager.CreateRoomClicked += CreateRoomClicked;
    }

    private void ProfileButtonClicked()
    {
        if (profilePanel.activeSelf)
        {
            gamePanel.SetActive(true);
            topPanel.SetActive(true);
            createRoomPanel.SetActive(false);
            profilePanel.SetActive(false);
        }
        else
        {
            gamePanel.SetActive(false);
            topPanel.SetActive(false);
            createRoomPanel.SetActive(false);
            profilePanel.SetActive(true);
        }
        
    }

    private void CreateRoomExitClicked()
    {
        gamePanel.SetActive(true);
        topPanel.SetActive(true);
        createRoomPanel.SetActive(false);
    }

    private void OnDisable()
    {
        EventManager.ProfileButtonClicked -= ProfileButtonClicked;
        EventManager.CreateRoomExitClicked -= CreateRoomExitClicked;
        EventManager.CreateRoomClicked -= CreateRoomClicked;
    }

    private void CreateRoomClicked()
    {
        gamePanel.SetActive(false);
        topPanel.SetActive(false);
        createRoomPanel.SetActive(true);
    }

    private void Start()
    {
        var playerData = EventManager.GetPlayerData();
        playerUsername.text = playerData.username;
        playerMoney.text = playerData.money.ToString();
    }
}
