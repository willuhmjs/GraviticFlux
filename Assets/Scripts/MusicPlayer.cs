using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip clipMainMenu;
    public AudioClip clipLevels;
    AudioSource source;
    private static MusicPlayer instance = null;
    private bool isContinuingMusic = false;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        source = GetComponent<AudioSource>();
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        if (next.name == "Level0")
        {
            if(isContinuingMusic)
            {
                source.Stop();
                isContinuingMusic = false;
            }
            source.clip = clipMainMenu;
            source.Play();
        }
        else
        {
            if(!isContinuingMusic)
            {
                source.clip = clipLevels;
                source.Play();
                isContinuingMusic = true;
            }
        }
    }
}
