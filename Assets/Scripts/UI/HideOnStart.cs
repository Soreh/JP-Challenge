using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideOnStart : MonoBehaviour
{
    public GameStyle style;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(GameManager.Instance.Style == style ? true : false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
