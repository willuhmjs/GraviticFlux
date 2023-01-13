using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

