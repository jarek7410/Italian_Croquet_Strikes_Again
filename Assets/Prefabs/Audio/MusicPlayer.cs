using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    void Start()
    {
        var audioSource = GetComponent<AudioSource>();
        audioSource.Play();
        audioSource.loop = true;
        DontDestroyOnLoad(gameObject);
    }
}
