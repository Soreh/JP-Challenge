using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIMainHandler : MonoBehaviour
{
    public GameObject InputNamePanel;
    private MainManager MainMngr;

    public TMP_InputField NameField;
    public Button SaveScoreButton;

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
        int score = MainManager.Instance.GetPoints();
        string name = NameField.text;
        Debug.Log(name + ": " + score);
        DismissInputName();
    }

    public void SetSaveButton()
    {
        SaveScoreButton.interactable = NameField.text != "" ? true : false;
    }


}
