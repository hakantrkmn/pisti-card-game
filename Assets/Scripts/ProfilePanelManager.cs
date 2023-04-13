using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProfilePanelManager : MonoBehaviour
{
    public TextMeshProUGUI usernameText;

    public TextMeshProUGUI winAmountText;
    public TextMeshProUGUI loseAmountText;
    public TextMeshProUGUI moneyAmountText;

    void Start()
    {
        var playerData = EventManager.GetPlayerData();

        usernameText.text = playerData.username;
        winAmountText.text = playerData.winAmount.ToString();
        loseAmountText.text = playerData.loseAmount.ToString();
        moneyAmountText.text = playerData.money.ToString();
        
    }
    
}
