using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [HideInInspector] public Image timerUIFill;
    [HideInInspector] public TextMeshProUGUI timerText;
    [HideInInspector] public TextMeshProUGUI speakText;
    [HideInInspector] public float maxTime;
    [HideInInspector] public List<GameObject> genangans;
    [HideInInspector] public int numOfShowingGenangan;
    [HideInInspector] public float[] splashTimes;
    [HideInInspector] public int lastTriggeredTimeIndex;
    [HideInInspector] public int lastTime;
    [HideInInspector] public Countdown countdown;
    [HideInInspector] public AudioManager audioManager;
    [HideInInspector] public MissionManager missionManager;
    void Awake(){   
        missionManager = FindObjectOfType<MissionManager>();
        audioManager = FindObjectOfType<AudioManager>();
        timerUIFill = transform.GetChild(0).GetComponent<Image>();
        timerText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        countdown = FindObjectOfType<Countdown>();
        genangans = new List<GameObject>(GameObject.FindGameObjectsWithTag("object-genangan"));
        speakText = GameObject.FindGameObjectWithTag("text-speak").GetComponent<TextMeshProUGUI>();
        speakText.gameObject.SetActive(false);
        foreach(GameObject genangan in genangans){
            genangan.SetActive(false);
        }
        splashTimes = new float[]{0.75f,  0.5f, 0.25f, float.MinValue };
        numOfShowingGenangan = genangans.Count/(splashTimes.Length-1);
    }

    public void StartTime(Action _callback, float _maxTime){
        maxTime = _maxTime;
        lastTriggeredTimeIndex = 0;
        LeanTween.value(gameObject, TimeUpdateCallback,maxTime, 0, maxTime).setOnComplete(_callback);
    }

    void TimeUpdateCallback(float _time){
        timerUIFill.fillAmount = _time/maxTime;
        int timeInt = Mathf.CeilToInt(_time);
        if(lastTime != timeInt){
            lastTime = timeInt;
            string timeString = timeInt.ToString();
            timerText?.SetText(timeString);
            if(lastTime > 0 &&lastTime <= 9){
                countdown.Show(timeString);
            }
        }
        if(timerUIFill.fillAmount < splashTimes[lastTriggeredTimeIndex]){
            lastTriggeredTimeIndex+=1;
            ShowSplash();
        }
    }

    void ShowSplash(){
        audioManager.PlaySplash();
        speakText.SetText(missionManager.GetRandomActiveMission().missionData.missionUrgentNarasi);
        speakText.gameObject.SetActive(true);
        LeanTween.delayedCall(3f,()=>speakText.gameObject.SetActive(false));
        for(int i=0;i<numOfShowingGenangan;i++){
            int randIndex = UnityEngine.Random.Range(0,genangans.Count);
            Debug.Log(randIndex);
            genangans[randIndex].SetActive(true);
            genangans.RemoveAt(randIndex);
        }
    }
}
