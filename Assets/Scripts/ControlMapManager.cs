using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlMapManager : MonoBehaviour
{
    private Button button;
    private TMP_Text buttonText;
    private bool isSelected;

    public PlayerAction playerAction;

    private void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TMP_Text>();
        button.onClick.AddListener(ToggleButton);
        UpdateFromSettings();
    }
    
    void OnEnable() => UpdateFromSettings();

    void OnDisable() {
        button.interactable = true; 
        isSelected = false; 
    }

    void UpdateFromSettings() {
        SettingsData data = DataManagement.LoadSettings();
        if (buttonText) {
            buttonText.text = data.controls[playerAction].ToString();
        }
    }

    private void Update()
    {
        if (isSelected)
        {
            // Check for input and update the button text accordingly
            foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (key == KeyCode.Escape || key.ToString().Contains("Mouse")) continue;                
                if (Input.GetKeyDown(key))
                {
                    buttonText.text = key.ToString();
                    isSelected = false;
                    button.interactable = true;

                    SettingsData data = DataManagement.LoadSettings();
                    data.controls[playerAction] = key;
                    DataManagement.SaveSettings(data);
                    break;
                }
            }
        } else UpdateFromSettings();
    }

    private void ToggleButton()
    {
        isSelected = !isSelected;
        button.interactable = !isSelected;
    }
}
