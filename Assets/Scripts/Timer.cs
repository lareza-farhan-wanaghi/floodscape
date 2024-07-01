using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [HideInInspector] public Image timerUIFill;
    [HideInInspector] public TextMeshProUGUI timerText;
    [HideInInspector] public float maxTime;
    [HideInInspector] public List<GameObject> genangans;
    [HideInInspector] public int numOfShowingGenangan;
    [HideInInspector] public float[] splashTimes = new float[]{0.25f, 0.5f, 0.75f, float.MaxValue};
    [HideInInspector] public int lastTriggeredTimeIndex;

    void Awake(){   
        timerUIFill = transform.GetChild(0).GetComponent<Image>();
        timerText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        genangans = new List<GameObject>(GameObject.FindGameObjectsWithTag("object-genangan"));
        foreach(GameObject genangan in genangans){
            genangan.SetActive(false);
        }
        numOfShowingGenangan = genangans.Count/(splashTimes.Length-1);
    }

    public void StartTime(Action _callback, float _maxTime){
        maxTime = _maxTime;
        lastTriggeredTimeIndex = 0;
        LeanTween.value(gameObject, TimeUpdateCallback,maxTime, 0, maxTime).setOnComplete(_callback);
    }

    void TimeUpdateCallback(float _time){
        timerUIFill.fillAmount = (maxTime - _time)/maxTime;
        timerText?.SetText(Mathf.CeilToInt(_time).ToString());
        if(timerUIFill.fillAmount > splashTimes[lastTriggeredTimeIndex]){
            lastTriggeredTimeIndex+=1;
            ShowSplash();
        }
    }

    void ShowSplash(){
        for(int i=0;i<numOfShowingGenangan;i++){
            int randIndex = UnityEngine.Random.Range(0,genangans.Count);
            genangans[randIndex].SetActive(true);
            genangans.RemoveAt(randIndex);
        }
    }
}
