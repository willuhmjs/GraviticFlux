using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class SettingsMenu : MonoBehaviour
{
    private static SettingsMenu instance;
    public Button[] tabs;
    public Image[] panels;
    private int activeTabIndex = 0;

    // stores the buttons in the LevelSelectPanel child of this gameobject
    public Button[] levelButtons;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        levelButtons = GameObject.Find("LevelSelectArea").GetComponentsInChildren<Button>();
    }


    // Start is called before the first frame update
    void Start()
    {
        SwitchTab(0);
        gameObject.SetActive(false);
    }

    public void ExitMenu() {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void SwitchTab(int index)
    {
        UpdateLevelButtons();
        tabs[activeTabIndex].interactable = true;
        tabs[index].interactable = false;
        panels[activeTabIndex].gameObject.SetActive(false);
        panels[index].gameObject.SetActive(true);
        activeTabIndex = index;
    }

    public void GoToLevel(Button button) {
        SceneManager.LoadScene(button.name);
        ExitMenu();
    }

    void OnEnable() {
        UpdateLevelButtons();
    }

    void UpdateLevelButtons() {
        int latestLevel = DataManagement.LoadSettings().latestLevel;
        for (int i = 0; i < levelButtons.Length; i++) {
            if (i <= latestLevel) {
                levelButtons[i].interactable = true;
            } else {
                levelButtons[i].interactable = false;
            }
        }
    }


    public void ResetGame() {
        DataManagement.SaveSettings(new SettingsData());
        SceneManager.LoadScene("Level0");
        ExitMenu();
    }

    // ControlMapManager is for edited buttons so this has to occur here
    public void ResetControls() {
        SettingsData data = DataManagement.LoadSettings();
        data.controls = new Dictionary<PlayerAction, KeyCode>() {
            {PlayerAction.MoveLeft, KeyCode.A},
            {PlayerAction.MoveRight, KeyCode.D},
            {PlayerAction.Jump, KeyCode.W},
            {PlayerAction.FlipGravity, KeyCode.Space},
            {PlayerAction.Reset, KeyCode.R}
        };
        DataManagement.SaveSettings(data);
    }
}
