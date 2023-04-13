using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamePanelController : MonoBehaviour
{
    public int roomIndex;
    public TextMeshProUGUI roomNameText;

    private void Start()
    {
        SetRoomName();
    }

    private void OnEnable()
    {
        EventManager.PlayButtonClicked += PlayButtonClicked;
        EventManager.RoomArrowClicked += RoomArrowClicked;
    }

    private void PlayButtonClicked()
    {
        var roomData = EventManager.GetRoomData();
        var playerData = EventManager.GetPlayerData();
        roomData.roomIndex = roomIndex;
        if (playerData.money >roomData.rooms[roomIndex].maxBet)
        {
            roomData.bet = roomData.rooms[roomIndex].maxBet;

        }
        else
        {
            roomData.bet = playerData.money;

        }
        SceneManager.LoadScene(1);

    }


    private void OnDisable()
    {
        EventManager.PlayButtonClicked -= PlayButtonClicked;
        EventManager.RoomArrowClicked -= RoomArrowClicked;
    }

    private void RoomArrowClicked(int side)
    {
        var roomData = EventManager.GetRoomData();
        if (side==0)
        {
            if (roomIndex!=0)
            {
                roomIndex--;
                SetRoomName();
            }
        }
        else
        {
            if (roomIndex!=roomData.rooms.Count-1)
            {
                roomIndex++;
                SetRoomName();
            }
        }

        roomData.roomIndex = roomIndex;
    }

    public void SetRoomName()
    {
        roomNameText.text = EventManager.GetRoomData().rooms[roomIndex].roomName;
    }
}
