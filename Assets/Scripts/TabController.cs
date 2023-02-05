using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    public Button[] tabs;
    public Image[] panels;
    private int activeTabIndex = 0;

    void Start() => SwitchTab(0);

    public void SwitchTab(int index)
    {
        tabs[activeTabIndex].interactable = true;
        tabs[index].interactable = false;
        panels[activeTabIndex].gameObject.SetActive(false);
        panels[index].gameObject.SetActive(true);
        activeTabIndex = index;
    }
}
