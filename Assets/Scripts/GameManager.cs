using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
    private int _scoreToSave = 3;
    private string _scoresSavePath;

    private void Start() {

    }

    private void Awake() {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        } else {
            Instance = this;
            _scoresSavePath = Application.persistentDataPath + "/savedscores.json";
            ScoresList = new List<Score>();
            GetSavedScores();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    private void SaveScores()
    {
        SavedScores save = new SavedScores();
        save.scores = new Score[ScoresList.Count];
        for (int i = 0; i < ScoresList.Count; i++)
        {
            save.scores[i] = ScoresList[i];
        }
        string json = JsonUtility.ToJson(save);
        File.WriteAllText(_scoresSavePath, json );
        Debug.Log(json + " saved to : " + _scoresSavePath);
    }

    public void GetSavedScores()
    {
        if (File.Exists(_scoresSavePath))
        {
            string json = File.ReadAllText(_scoresSavePath);
            SavedScores save = JsonUtility.FromJson<SavedScores>(json);
            Debug.Log("Scores retrieved at " + _scoresSavePath);
            foreach (Score score in save.scores)
            {
                ScoresList.Add(score);
            }
            SortScores();
        }
    }

    public string PrintScore()
    {
        string scores = "The " + _scoreToSave + "<br>Highest Scores :<br>";
        if (ScoresList.Count > 0) {
            foreach (Score score in ScoresList)
            {
                scores += "- " + score.name + " / " + score.score + "<br>";
            }
            if (ScoresList.Count < _scoreToSave) 
            {
                int emptyLinesToPrint = _scoreToSave - ScoresList.Count;
                for (int i = 0; i < emptyLinesToPrint; i++)
                {
                    scores += "- " + "No one yet..." + "<br>";
                }
            }
        } else {
            scores += "None yet...";
        } 
        return scores;
    }

    [System.Serializable]
    public class SavedScores
    {
        public Score[] scores;
    }

    [System.Serializable]
    public class Score
    {
        public string name;
        public int score;
    }

    public void AddNewScore(string name, int score)
    {
        Score scoreToAdd = new Score();
        scoreToAdd.name = name;
        scoreToAdd.score = score;
        ScoresList.Add(scoreToAdd);
        SortScores();
        if (ScoresList.Count > _scoreToSave)
        {
            ScoresList.RemoveAt(ScoresList.Count - 1);
        }
    }

    ///<summary>
    ///Check if a score deserves to enter the pantheon and return the rank,
    ///returns 0 otherwise.
    ///</summary>
    public int GetScoreRank(int score)
    {
        int rank = 0;
        bool rankOverride = false;
        for (int i = 0; i < ScoresList.Count; i++)
        {
            if (score > ScoresList[i].score)
            {
                rank = i + 1;
                rankOverride = true;
                break;
            }
        }
        if (!rankOverride && ScoresList.Count < _scoreToSave) {
            rank = ScoresList.Count + 1;
        }
        return rank;
    }

    private void SortScores()
    {
        if (ScoresList.Count > 0)
        {
            ScoresList.Sort((x, y) => y.score.CompareTo(x.score));
        }
    }

    public void QuitGame()
    {
        SaveScores();
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
