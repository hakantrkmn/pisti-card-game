using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameCanvasController : MonoBehaviour
{
    public GameObject sidePanel;
    public GameObject gameOverPanel;
    public TextMeshProUGUI gameOverText;
    private void OnEnable()
    {
        EventManager.SetLose += SetLose;
        EventManager.SetWin += SetWin;
        EventManager.SideMenuClicked += SideMenuClicked;
    }

    private void SetLose()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "Game Lose";
    }

    private void SetWin()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = "Game Win";
    }

    private void OnDisable()
    {
        EventManager.SetLose -= SetLose;
        EventManager.SetWin -= SetWin;
        EventManager.SideMenuClicked -= SideMenuClicked;
    }

    private void SideMenuClicked()
    {
        if (sidePanel.activeSelf)
        {
            sidePanel.SetActive(false);

        }
        else
        {
            sidePanel.SetActive(true);

        }
    }
    
}
