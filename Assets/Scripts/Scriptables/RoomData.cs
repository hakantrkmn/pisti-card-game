using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu]
public class RoomData : ScriptableObject
{
   public int maxPoint;
   public GameObject roomPrefab;
   public int roomIndex;
   public List<Room> rooms;

   public int aiAmount;
   public int bet;
}
