using System.Collections;
using System.Collections.Generic;
using ElRaccoone.Tweens;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveScript : MonoBehaviour
{
    int currentScene;
    int numScenes;

    GameObject transition;
    CanvasGroup canvasGroup;
    Renderer rend;
    AudioSource audioSource;

    public AudioClip levelEnter;
    public AudioClip levelComplete;

    // Start is called before the first frame update
    void Start()
    {

        currentScene = SceneManager.GetActiveScene().buildIndex;
        numScenes = SceneManager.sceneCountInBuildSettings;
        
        transition = GameObject.Find("Transition");
        canvasGroup = transition.GetComponent<CanvasGroup>();

        audioSource = GetComponent<AudioSource>();

        rend = GetComponent<Renderer>();

        if (currentScene != 0) {
            canvasGroup.alpha = 1;
            canvasGroup.TweenCanvasGroupAlpha(0, 1f);
            audioSource.clip = levelEnter;
            audioSource.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            // make renderer inactive
            rend.enabled = false;
            NextLevel(0.75f);
        }
    }

    public void NextLevel(float transitionTime) {
        StartCoroutine(NextLevelIEnumerator(transitionTime));
    }

    public IEnumerator NextLevelIEnumerator(float transitionTime) {
        audioSource.clip = levelComplete;
        audioSource.Play();
        canvasGroup.TweenCanvasGroupAlpha(1, transitionTime);
        yield return new WaitForSeconds(transitionTime);
        //dataManagement.latestLevel++;
        if (currentScene < numScenes - 1) {
            SceneManager.LoadScene("Level" + (currentScene + 1));
        } else {
            SceneManager.LoadScene("Level0");
        }
    }


}
