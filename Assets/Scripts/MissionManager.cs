using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public GameObject missionUIPrefab;
    [HideInInspector] public Action onCompleted;
    [HideInInspector] public Dictionary<string, ActiveMission> activeMissions = new Dictionary<string, ActiveMission> ();

    public void Init(Action _onCompletedCallback, MissionData[] _missionDatas){
        for(int i=0;i<_missionDatas.Length;i++){
            GameObject ui = Instantiate(missionUIPrefab, transform);
            ui.GetComponent<Image>().sprite = _missionDatas[i].missionSprite;
            activeMissions.Add(_missionDatas[i].missionName,new ActiveMission(_missionDatas[i],ui,false));
        }
        onCompleted = _onCompletedCallback;
    }

    public void CheckMission(ItemData _itemData){
        foreach(ActiveMission missionOnRoll in activeMissions.Values){
            if(!missionOnRoll.isCompleted && missionOnRoll.missionData.itemData == _itemData){
                missionOnRoll.MarkCompleted();
                LeanTween.delayedCall(0.5f, CheckAllMissions);
                break;
            }
        }
    }

    void CheckAllMissions() {
         foreach(ActiveMission missionOnRoll in activeMissions.Values){
            if(!missionOnRoll.isCompleted){
                return;
            }
        }
        onCompleted.Invoke();
    }

    public class ActiveMission{
        public MissionData missionData;
        public GameObject ui;
        public bool isCompleted;
        public ActiveMission(MissionData _missionData, GameObject _ui, bool _isCompleted){
            missionData=_missionData;
            ui=_ui;
            isCompleted=_isCompleted;
        }

        public void MarkCompleted(){
            isCompleted = true;
            ui.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
