using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource buttonAudio;
    public AudioSource loseAudio;
    public AudioSource winAudio;
    public AudioSource collectAudio;
    public AudioSource splashAudio;

    public void PlayButton(){
        buttonAudio.Play();
    }

    public void PlayLose(){
        loseAudio.Play();
    }

    public void PlayWin(){
        winAudio.Play();
    }

    public void PlayCollect(){
        collectAudio.Play();
    }

    public void PlaySplash(){
        splashAudio.Play();
    }
}
