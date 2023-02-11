using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    private static SettingsMenu instance;
    public Button[] tabs;
    public Image[] panels;
    private int activeTabIndex = 0;
    private DataManagement dataManagement;

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

        dataManagement = gameObject.GetComponent<DataManagement>();
        levelButtons = GameObject.Find("LevelSelectPanel").GetComponentsInChildren<Button>();
    }


    // Start is called before the first frame update
    void Start()
    {
        UpdateLevelButtons();
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
        for (int i = 0; i < levelButtons.Length; i++) {
            if (i <= dataManagement.GetLatestLevel()) {
                levelButtons[i].interactable = true;
            } else {
                levelButtons[i].interactable = false;
            }
        }
    }
}
