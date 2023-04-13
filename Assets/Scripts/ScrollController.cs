using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ScrollController : MonoBehaviour
{
    ScrollRect scroll;
    public Transform content;
    public float cellSizeX;
    public float cellSizeY;
    public float scrollValue = 0;

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

        if (side == 0)
        {
            if (scrollValue > 0)
            {
                Sequence scrollTween = DOTween.Sequence();
                scrollTween.Append(DOVirtual.Float(scrollValue, scrollValue - (float)1 / (roomData.rooms.Count - 1),
                    .3f,
                    (x) =>
                    {
                        if (scrollValue > 0)
                        {
                            scrollValue = x;
                        }
                    }));
            }
        }
        else
        {
            if (scrollValue < 1)
            {
                Sequence scrollTween = DOTween.Sequence();
                scrollTween.Append(DOVirtual.Float(scrollValue, scrollValue + (float)1 / (roomData.rooms.Count - 1),
                    .3f,
                    (x) =>
                    {
                        if (scrollValue < 1)
                        {
                            scrollValue = x;
                        }
                    }));
            }
        }
    }

    IEnumerator WaitUntilEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        SetCellSize();
    }

    public void SetCellSize()
    {
        cellSizeX = content.parent.GetComponent<RectTransform>().rect.width;
        cellSizeY = content.parent.GetComponent<RectTransform>().rect.height;
        content.GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellSizeX, cellSizeY);
    }

    private void Start()
    {
        scroll = GetComponent<ScrollRect>();
        StartCoroutine(WaitUntilEndOfFrame());


        SetRooms();
    }

    private void Update()
    {
        scroll.horizontalNormalizedPosition = scrollValue;
    }


    public void SetRooms()
    {
        var roomData = EventManager.GetRoomData();

        foreach (var room in roomData.rooms)
        {
            var tempRoom = Instantiate(roomData.roomPrefab, Vector3.zero, quaternion.identity, content)
                .GetComponent<RoomController>();
            tempRoom.SetRoom(room);
        }
    }
}