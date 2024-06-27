using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MinigameBase : MonoBehaviour{
    Action onSuccessCallback;

    public void Play(Action _onSuccessCallback){
        onSuccessCallback=_onSuccessCallback;
        gameObject.SetActive(true);
    }    

    public void Complete(){
        onSuccessCallback.Invoke();
        gameObject.SetActive(false);
    }
}