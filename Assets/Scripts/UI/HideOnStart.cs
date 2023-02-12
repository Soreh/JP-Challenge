using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>Set the visibilty of the game object at runtime, based on Game Style</summary>
[DefaultExecutionOrder(1000)]
public class HideOnStart : MonoBehaviour
{
    public GameStyle style;
    [SerializeField] bool forceHidingAtStart = false;
    // Start is called before the first frame update
    void Awake()
    {
        if (forceHidingAtStart && gameObject.activeInHierarchy) 
        {
            gameObject.SetActive(false);
        } else if (GameManager.Instance != null ) {
            if(style == GameManager.Instance.Style) {
                gameObject.SetActive(true);
            } else {
                gameObject.SetActive(false);
            }
        }
    }
}
