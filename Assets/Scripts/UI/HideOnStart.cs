using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1000)]
public class HideOnStart : MonoBehaviour
{
    public GameStyle style;
    [SerializeField] bool forceHidingAtStart = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (gameObject.activeInHierarchy)
        {
            if (forceHidingAtStart) 
            {
                Debug.Log(gameObject.name + " forced to hide");
                gameObject.SetActive(false);
            } else if (GameManager.Instance != null ) {
                if(style == GameManager.Instance.Style) {
                    Debug.Log(GameManager.Instance.Style + " = " + style);
                    gameObject.SetActive(true);
                    Debug.Log(gameObject.name + " activated");
                } else {
                    gameObject.SetActive(false);
                    Debug.Log(gameObject.name + " deactivated");
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
