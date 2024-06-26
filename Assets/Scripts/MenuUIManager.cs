using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuUIManager : MonoBehaviour
{
    public TMP_InputField playerName;
    public TMP_Text bestScoreText;

    public void Start()
    {
        if (MainManager.Instance.bestScoreName != "" && MainManager.Instance.bestScorePoints > 0)
        {
            bestScoreText.text =
                $"Best Score : {MainManager.Instance.bestScoreName} : {MainManager.Instance.bestScorePoints}";
        }
    }

    public void UpdateName()
    {
        MainManager.Instance.SetPlayerName(playerName.text);
    }
}
