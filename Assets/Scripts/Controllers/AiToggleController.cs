using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiToggleController : MonoBehaviour
{
    public void ToggleActivated(int amount)
    {
        var roomData = EventManager.GetRoomData();
        roomData.aiAmount = amount;
    }
}
