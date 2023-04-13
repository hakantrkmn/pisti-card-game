using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateGamePanelManager : MonoBehaviour
{
    public Slider slider;

    public TextMeshProUGUI currentBet;
    public TextMeshProUGUI minBet;
    public TextMeshProUGUI maxBet;

    private void OnEnable()
    {
        EventManager.CreateTableClicked += CreateTableClicked;
    }

    private void OnDisable()
    {
        EventManager.CreateTableClicked -= CreateTableClicked;
    }

    private void CreateTableClicked()
    {
        var roomData = EventManager.GetRoomData();
        roomData.bet = (int)slider.value;
        SceneManager.LoadScene(1);
    }


    private void Start()
    {
        SetCreateRoomPanel();
    }

    private void SetCreateRoomPanel()
    {
        var room = EventManager.GetRoomData().rooms[EventManager.GetRoomData().roomIndex];
        minBet.text = room.minBet.ToString();
        maxBet.text = room.maxBet.ToString();

        slider.minValue = room.minBet;
        slider.maxValue = room.maxBet;
    }

    void Update()
    {
        currentBet.text = ((int)slider.value).ToString();
    }
}
