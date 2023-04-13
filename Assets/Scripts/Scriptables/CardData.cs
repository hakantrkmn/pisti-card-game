using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CardData : ScriptableObject
{
    public GameObject cardPrefab;
    public List<CardStats> cards;
    public List<CardStats> specialCards;

}
