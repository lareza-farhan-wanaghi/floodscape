using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource buttonAudio;
    public AudioSource loseAudio;
    public AudioSource winAudio;
    public AudioSource collectAudio;

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
}
