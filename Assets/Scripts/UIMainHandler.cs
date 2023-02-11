using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(1000)]
public class UIMainHandler : MonoBehaviour
{
    public GameObject InputNamePanel;
    private MainManager MainMngr;

    public TMP_InputField NameField;
    public Button SaveScoreButton;
    public GameObject HighestScorePanel;
    public GameObject GameOverPanel;
    public Text ScoreText;
    public TextMeshProUGUI BestScoresTxt;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Awake() {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToStartScreen()
    {
        SceneManager.LoadScene(0);
    }

    public void DismissInputName()
    {
        InputNamePanel.SetActive(false);
    }

    public void SaveScore()
    {
        string name = NameField.text;
        MainManager.Instance.AddScoreToList(name);
        DismissInputName();
    }

    public void SetSaveButton()
    {
        SaveScoreButton.interactable = NameField.text != "" ? true : false;
    }

    public void ShowGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }
    public void ShowHighestScorePanel(int rank)
    {
        HighestScorePanel.GetComponentInChildren<TextMeshProUGUI>().text = "Great !<br>"
            + "You've just entered the pantheon of highest scores on the "
            + RankToString(rank) + " place !<br>"
            + "Give us your name !";
        HighestScorePanel.SetActive(true);
    }

    private string RankToString(int rank)
    {
        switch (rank)
        {
            case 1 :
                return "1st";
            case 2 :
                return "2nd";
            case 3 :
                return "3rd";
            default:
                return rank + "th";
        }
    }

    public void UpdateScore(int score)
    {
        ScoreText.text = $"Score : {score}"; // Interesting notation
    }

    public void UpdateScoresText()
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
