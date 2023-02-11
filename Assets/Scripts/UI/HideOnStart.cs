using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnStart : MonoBehaviour
{
    public GameStyle style;
    [SerializeField] bool forceHidingAtStart = false;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.active)
        {
            if (forceHidingAtStart) 
            {
                gameObject.SetActive(false);
            } else if (GameManager.Instance == null ) {
                gameObject.SetActive(GameManager.Instance.Style == style ? true : false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
