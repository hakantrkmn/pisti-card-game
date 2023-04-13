using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AIPanelManager : MonoBehaviour
{
    public AICardPanelController cardPanel;
    public TextMeshProUGUI usernameText;
    public TextMeshProUGUI betText;
    public TextMeshProUGUI scoreText;
    public int score;
    void Start()
    {
        var cellSize = new Vector2(cardPanel.GetComponent<RectTransform>().rect.width / 4,
            cardPanel.GetComponent<RectTransform>().rect.height);
        cardPanel.GetComponent<GridLayoutGroup>().cellSize = cellSize;

        var profile = EventManager.GetAiProfiles().profiles[EventManager.GetAiProfiles().profileIndex];
        EventManager.GetAiProfiles().profileIndex++;
        if (EventManager.GetAiProfiles().profileIndex>=EventManager.GetAiProfiles().profiles.Count)
        {
            EventManager.GetAiProfiles().profileIndex = 0;
        }
        var roomData = EventManager.GetRoomData();
        usernameText.text = profile.username;
        betText.text = roomData.bet.ToString();
        scoreText.text = score.ToString();
    }

    public void UpdateScore(int increase)
    {
        score = increase;
        scoreText.text = score.ToString();

    }

   
}
