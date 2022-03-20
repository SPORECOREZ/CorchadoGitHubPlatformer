using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinAudio : MonoBehaviour
{

    public AudioClip backgroundMusic;
    public AudioClip winSound;
    public AudioSource musicSource;
    public GameObject winText;
    private bool winState;

    // Start is called before the first frame update
    void Start()
    {
        winState = false;
        musicSource.clip = backgroundMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (winText.activeSelf)
        {
            PlayWin();
        }
    }

    void PlayWin()
    {
        if (winState == false)
        {
            winState = true;
            musicSource.clip = winSound;
            musicSource.loop = false;
            musicSource.Play();
        }
    }
}
