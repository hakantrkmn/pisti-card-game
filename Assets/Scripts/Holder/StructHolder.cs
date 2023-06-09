using UnityEngine;
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Unity.VisualScripting;

[Serializable]
public struct TutorialPanels
{
    public TutorialTypes panelName;
    public GameObject panelGameObject;
}

[Serializable]
public struct Room
{
    public string roomName;
    public int minBet;
    public int maxBet;

}

[Serializable]
public class CardStats
{
    public CardTypes cardType;
    public Sprite image;
    public string value;
}

[Serializable]
public class AIProfile
{
    public Sprite photo;
    public string username;
}

#region Incremental Idle
[Serializable]
public class IncrementalIdleValues
{
    public int currentUpgradeLevel;
    public float totalUpgradeGainValue;
    public bool isMaximized;

    public void ResetValues()
    {
        currentUpgradeLevel = 0;
        totalUpgradeGainValue = 0;
        isMaximized = false;
    }
}

[Serializable]
public class PriceHolderStruct
{
    public string name;
    public List<PriceAnodValue> priceAndValueList = new List<PriceAnodValue>();

    public PriceHolderStruct(string _name, int _length, int _multiplier)
    {
        name = _name;

        for (int i = 0; i < _length; i++)
            priceAndValueList.Add(new PriceAnodValue((i + 1) * _multiplier, (i + 1)));
    }
}

[Serializable]
public class PriceAnodValue
{
    public int requiredMoneyValue;
    public float upgradeAmount;

    public PriceAnodValue(int _requiredMoneyValue, float _upgradeAmount)
    {
        this.requiredMoneyValue = _requiredMoneyValue;
        this.upgradeAmount = _upgradeAmount;
    }
}
#endregion