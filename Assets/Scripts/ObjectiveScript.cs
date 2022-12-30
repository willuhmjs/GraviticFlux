using System.Collections;
using System.Collections.Generic;
using ElRaccoone.Tweens;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectiveScript : MonoBehaviour
{
    int currentScene;
    int numScenes;

    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        numScenes = SceneManager.sceneCountInBuildSettings;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Player") {
            gameObject.TweenSpriteRendererAlpha(0, 0.5f);
            NextLevel();
        }
    }

    public void NextLevel() {
        if (currentScene < numScenes - 1) {
            SceneManager.LoadScene("Level" + (currentScene + 1));
        } else {
            SceneManager.LoadScene("Level0");
        }
    }


}
