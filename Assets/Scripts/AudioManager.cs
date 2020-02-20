using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [Header("Background music stuff")]
    public AudioClip[] backgroundMusics;
    public AudioSource bgMusicAudioSource;


    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!bgMusicAudioSource.isPlaying)
        {
            bgMusicAudioSource.clip = GetRandom();
            bgMusicAudioSource.Play();
        }

    }

    AudioClip GetRandom()
    {
        return backgroundMusics[Random.Range(0, backgroundMusics.Length)];
    }
}
