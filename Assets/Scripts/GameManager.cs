using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameStyle
{
    OldSchool,
    Vintage
}
public class GameManager : MonoBehaviour
{
    public GameStyle Style;
    public List<Score> ScoresList;
    public static GameManager Instance;

    private void Start() {
        ScoresList = GetScoreList();
    }

    private void Awake() {
        if (Instance != null)
        {
            return;
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    public void SaveScores()
    {
        // TODO
    }

    public Score[] GetSavedScores()
    {
        Score mockup1 = new Score();
        mockup1.name = "Test_name";
        mockup1.score = 50;
        Score mockup2 = new Score();
        mockup2.name = "Test_name_2";
        mockup2.score = 30;
        Score mockup3 = new Score();
        mockup3.name = "Test_name_3";
        mockup3.score = 20;

        Score[] scores = new Score[3];
        scores[0] = mockup1;
        scores[1] = mockup2;
        scores[2] = mockup3;

        return scores;
    }

    public string PrintScore()
    {
        string scores = "";
        foreach (Score score in ScoresList)
        {
            scores += score.name + " : " + score.score + "<br>";
        }
        return scores;
    }

    public class SavedScores
    {
        public Score[] scores;
    }

    public class Score
    {
        public string name;
        public int score;
    }

    public void AddNewScore(Score scoreToAdd)
    {

    }

    public List<Score> GetScoreList()
    {
        List<Score> sortedScoreList = new List<Score>();
        Score[] scores = GetSavedScores();
        foreach (Score score in scores)
        {
            sortedScoreList.Add(score);
        }
        // TODO : sort the List
        return sortedScoreList;
    }
}
