using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[DefaultExecutionOrder(1000)]
public class BestScores : MonoBehaviour
{
    public TextMeshProUGUI BestScoresTxt;

    private void Awake() {
        SetScoresText();
    }

    ///<summary>Set the highest scores text</summary>
    void SetScoresText()
    {
        if (GameManager.Instance != null)
        {
            BestScoresTxt.text = GameManager.Instance.PrintScore();
        } else 
        {
            BestScoresTxt.text = "null";
            Debug.LogWarning("Game Manager is'nt instanciated");
        }
    }
}
