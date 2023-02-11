using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManagement : MonoBehaviour
{
    public void RefreshLevelStat() {
        //set latestlevel to storage determined by current level number
        if (PlayerPrefs.GetInt("latestLevel", 0) < SceneManager.sceneCountInBuildSettings) {
            PlayerPrefs.SetInt("latestLevel", SceneManager.GetActiveScene().buildIndex);
            PlayerPrefs.Save();
        }
    }

    public int GetLatestLevel() {
        RefreshLevelStat();
        return PlayerPrefs.GetInt("latestLevel", 0);
    }
}