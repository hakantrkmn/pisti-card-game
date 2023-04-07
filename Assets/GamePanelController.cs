using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        EventManager.RoomArrowClicked += RoomArrowClicked;
    }

   


    private void OnDisable()
    {
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
