using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControlMapManager : MonoBehaviour
{
    private Button button;
    private TMP_Text buttonText;
    private bool isSelected;

    private void Start()
    {
        button = GetComponent<Button>();
        buttonText = GetComponentInChildren<TMP_Text>();

        button.onClick.AddListener(ToggleButton);
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
                    break;
                }
            }
        }
    }

    private void ToggleButton()
    {
        isSelected = !isSelected;
        button.interactable = !isSelected;
    }
}
