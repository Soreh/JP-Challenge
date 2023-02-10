using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BestScores : MonoBehaviour
{
    public TextMeshProUGUI BestScoresTxt;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void Awake() {
        SetScoresText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
