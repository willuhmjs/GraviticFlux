using UnityEngine;
public class ButtonsScript : MonoBehaviour
{
    private static ButtonsScript instance;
    public GameObject settingsMenu;
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

    public void ExitGame() {
        #if (UNITY_EDITOR || DEVELOPMENT_BUILD)
            Debug.Log(this.name+" : "+this.GetType()+" : "+System.Reflection.MethodBase.GetCurrentMethod().Name); 
        #endif
        #if (UNITY_EDITOR)
            UnityEditor.EditorApplication.isPlaying = false;
        #elif (UNITY_STANDALONE) 
            Application.Quit();
        #elif (UNITY_WEBGL)
            Application.ExternalEval("location.reload()");
        #endif
    }

    public void OpenSettingsMenu() {
        settingsMenu.SetActive(true);
        Time.timeScale = 0;
    }
}
