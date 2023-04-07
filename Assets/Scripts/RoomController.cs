using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour
{
    public TextMeshProUGUI roomName;
    public int minBet;
    public int maxBet;
    public TextMeshProUGUI betRangeText;

    public Button playButton;
    public Button createRoomButton;
    public void SetRoom(Room roomObj)
    {
        roomName.text = roomObj.roomName;
        minBet = roomObj.minBet;
        maxBet = roomObj.maxBet;
        betRangeText.text = minBet.ToString() + "-" + maxBet.ToString();

        var playerData = EventManager.GetPlayerData();

        if (playerData.money < minBet)
        {
            playButton.interactable = false;
            createRoomButton.interactable = false;

        }
    }
}
