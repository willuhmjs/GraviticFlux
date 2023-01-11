using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerScript : MonoBehaviour
{
    GameObject Cube;
    CubeScript cubeScript;
    
    AudioSource audioSource;
    public AudioClip deathClip;

    // Start is called before the first frame update
    void Start()
    {
        Cube = GameObject.FindGameObjectWithTag("Player");
        cubeScript = Cube.GetComponent<CubeScript>();

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = deathClip;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player") cubeScript.Reset();
        audioSource.Play();
    }
}
