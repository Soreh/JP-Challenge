using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class StartMenuHandler : MonoBehaviour
{
    public GameObject OS_panel;
    public GameObject ARC_panel;
    public Slider StyleSlider;
    // Start is called before the first frame update
    void Start()
    {
        StyleSlider.value = GameManager.Instance.Style == GameStyle.OldSchool ? 0 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        GameManager.Instance.LoadGame();
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

    public void SetStyle()
    {
        int _style = Mathf.FloorToInt(StyleSlider.value);
        GameManager.Instance.Style = _style == 0 ? GameStyle.OldSchool : GameStyle.Vintage;
        if (GameManager.Instance.Style == GameStyle.OldSchool)
        {
            OS_panel.SetActive(true);
            ARC_panel.SetActive(false);
        } else if (GameManager.Instance.Style == GameStyle.Vintage) {
            OS_panel.SetActive(false);
            ARC_panel.SetActive(true);
        }
    }

    public void SetArcade()
    {
        StyleSlider.value = 1;
        SetStyle();
    }

    public void SetOldSchool()
    {
        StyleSlider.value = 0;
        SetStyle();
    }

}
