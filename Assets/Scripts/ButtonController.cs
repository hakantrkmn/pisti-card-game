using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    public ButtonTypes buttonType;

    public void ArrowButtonClicked(int side)
    {
        EventManager.RoomArrowClicked(side);
        
    }

    public void ButtonClicked()
    {
        switch (buttonType)
        {
            case ButtonTypes.CreateRoom:
                EventManager.CreateRoomClicked();
                break;
            case ButtonTypes.SideMenu:
                EventManager.SideMenuClicked();
                break;
            case ButtonTypes.NewGame:
                EventManager.NewGameClicked();
                break;
            case ButtonTypes.BackToLobby:
                EventManager.BackToLobbyClicked();
                break;
            case ButtonTypes.CreateTable:
                EventManager.CreateTableClicked();
                break;
            case ButtonTypes.Play:
                EventManager.PlayButtonClicked();
                break;
            case ButtonTypes.CreateRoomExit:
                EventManager.CreateRoomExitClicked();
                break;
            case ButtonTypes.ProfileButton:
                EventManager.ProfileButtonClicked();
                break;
        }
    }
}
