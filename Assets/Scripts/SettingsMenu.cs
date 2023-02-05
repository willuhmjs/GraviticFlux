using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SettingsMenu : MonoBehaviour
{
    private static SettingsMenu instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ExitMenu() {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
