using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class MinigameManager : MonoBehaviour
{
    [SerializeField] MinigameBase[] minigames;
    PlayerController playerController;
    bool isCompletedMinigame;

    void Awake(){
        playerController = FindObjectOfType<PlayerController>();
        foreach(MinigameBase minigame in minigames){
            minigame.gameObject.SetActive(false);
        }
    }   

    public bool CheckForCompletion(ItemData _itemData, Action _onSuccessCallback){
        if(_itemData.minigameIndex < 0 || isCompletedMinigame){
            return true;
        }else{
            playerController.ToggleIsMoveable(false);
            minigames[_itemData.minigameIndex].Play(()=>{
                isCompletedMinigame = true;
                _onSuccessCallback.Invoke();
                playerController.ToggleIsMoveable(true);
            });
            return false;
        }
    }

}
