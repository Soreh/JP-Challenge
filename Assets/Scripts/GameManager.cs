using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

///<summary>Used to define the Game Style</summary>
public enum GameStyle
{
    OldSchool,
    Vintage
}

///<summary>
///The main manager for the game, responsible for settong the style, 
///saving data, persist settings between scenes, load and quit the game,
///and manage the score system.
///</summary>
public class GameManager : MonoBehaviour
{
    public GameStyle Style;
    public List<Score> ScoresList;
    public static GameManager Instance;
    private int _scoreToSave = 3;
    private string _scoresSavePath;

    private void Awake() {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        } else {
            Instance = this;
            _scoresSavePath = Application.persistentDataPath + "/saved_data.json";
            ScoresList = new List<Score>();
            GetSavedData();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }

    private void SaveScoresAndSettings()
    {
        SavedData save = new SavedData();
        save.scores = new Score[ScoresList.Count];
        for (int i = 0; i < ScoresList.Count; i++)
        {
            save.scores[i] = ScoresList[i];
        }
        save.styleSetting = Style;
        string json = JsonUtility.ToJson(save);
        File.WriteAllText(_scoresSavePath, json );
        Debug.Log(json + " saved to : " + _scoresSavePath);
    }

    public void GetSavedData()
    {
        if (File.Exists(_scoresSavePath))
        {
            string json = File.ReadAllText(_scoresSavePath);
            SavedData save = JsonUtility.FromJson<SavedData>(json);
            foreach (Score score in save.scores)
            {
                ScoresList.Add(score);
            }
            SortScores();
            Style = save.styleSetting;
        }
    }

    ///<summary>
    ///Prepare the text for the highest scores panel and return it.
    ///</summary>
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

    ///<summary>Inner class responsible to handle saved data</summary>
    [System.Serializable]
    public class SavedData
    {
        public Score[] scores;
        public GameStyle styleSetting;
    }

    ///<summary>Inner class responsible to handle saved score</summary>
    [System.Serializable]
    public class Score
    {
        public string name;
        public int score;
    }

    ///<summary>Add anew score to the scores list</summary>
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

    ///<summary>
    ///Sort the scores List in descending order based on the score.
    ///</summary>
    private void SortScores()
    {
        if (ScoresList.Count > 0)
        {
            ScoresList.Sort((x, y) => y.score.CompareTo(x.score));
        }
    }

    ///<summary>
    ///Quit the game and save.
    ///(Exit play mode if in editor mode)
    ///</summary>
    public void QuitGame()
    {
        SaveScoresAndSettings();
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
