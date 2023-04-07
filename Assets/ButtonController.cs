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
            case ButtonTypes.CreateRoomExit:
                EventManager.CreateRoomExitClicked();
                break;
            case ButtonTypes.ProfileButton:
                EventManager.ProfileButtonClicked();
                break;
        }
    }
}
