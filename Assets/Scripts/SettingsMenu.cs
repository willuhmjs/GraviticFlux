using UnityEngine;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    private static SettingsMenu instance;
    public Button[] tabs;
    public Image[] panels;
    private int activeTabIndex = 0;

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
        SwitchTab(0);
        gameObject.SetActive(false);
    }

    public void ExitMenu() {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void SwitchTab(int index)
    {
        tabs[activeTabIndex].interactable = true;
        tabs[index].interactable = false;
        panels[activeTabIndex].gameObject.SetActive(false);
        panels[index].gameObject.SetActive(true);
        activeTabIndex = index;
    }
}
