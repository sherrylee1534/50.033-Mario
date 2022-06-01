using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private Button _button;

    public bool _isMenuOn = true;

    // Start is called before the first frame update
    void Start()
    {
        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(StartButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        Time.timeScale = 0.0f;
    }

    public void StartButtonClicked()
    {
        _isMenuOn = false; // Turn off menu; Mario can move now

        foreach (Transform eachChild in transform)
        {
            if (eachChild.name != "Score")
            {
                //Debug.Log("Child found. Name: " + eachChild.name);
                
                // Disable them
                eachChild.gameObject.SetActive(false);
                Time.timeScale = 1.0f;
            }
        }
    }
}
