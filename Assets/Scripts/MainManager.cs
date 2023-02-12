using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance;
    public Camera OsCam;
    public Camera ArcadeCam;
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;
    public UIMainHandler UImain_OS;
    public UIMainHandler UImain_ARCADE;
    private UIMainHandler UImain;
    
    private bool m_Started = false;
    private int m_Points;
    

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Awake() {
        if (Instance != null) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        SetStyle();
        UImain.UpdateScoresText();
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        UImain.UpdateScore(m_Points);
    }

    public int GetPoints()
    {
        return m_Points;
    }

    public void AddScoreToList(string name)
    {
        GameManager.Instance.AddNewScore(name, m_Points);
        UImain.UpdateScoresText();
    }

    public void GameOver()
    {
        UImain.ShowGameOverPanel();
        int rank = GameManager.Instance.GetScoreRank(m_Points);
        if (rank> 0) {
            UImain.ShowHighestScorePanel(rank);
        }
    }

    ///<summary>
    ///Set the style of the UI.
    ///If the game Manager is not loaded, it activates the default Old Style UI style
    ///</summary>
    private void SetStyle()
    {
        if (GameManager.Instance != null) 
        {
            switch (GameManager.Instance.Style)
            {
                case GameStyle.OldSchool :
                    OsCam.gameObject.SetActive(true);
                    ArcadeCam.gameObject.SetActive(false);
                    UImain = UImain_OS;
                    break;
                case GameStyle.Vintage :
                    OsCam.gameObject.SetActive(false);
                    ArcadeCam.gameObject.SetActive(true);
                    UImain = UImain_ARCADE;
                    break;
            }
        } else {
            OsCam.gameObject.SetActive(true);
            ArcadeCam.gameObject.SetActive(false);
            UImain_OS.gameObject.SetActive(true);
            UImain = UImain_OS;
        }
    }
}
