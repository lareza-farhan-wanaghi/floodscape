using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class InteractManager : MonoBehaviour
{
    [HideInInspector] public InteractableItem interactableItem;
    [HideInInspector] public Button interactButton;
    [HideInInspector] public Backpack backpack;
    [HideInInspector] public Action<ItemData> onInteract;
    [HideInInspector] public MinigameManager minigameManager;
    [HideInInspector] public AudioManager audioManager;
    
    void  Awake(){
        interactButton = GetComponent<Button>();
        backpack = FindObjectOfType<Backpack>();
        minigameManager = FindObjectOfType<MinigameManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Init(Action<ItemData> _onInteract){
        onInteract =_onInteract;
    }

    public void Interact(){
        audioManager.PlayButton();
        if(backpack.IsAvailable(interactableItem.data)){
            if(minigameManager == null || minigameManager.CheckForCompletion(interactableItem.data, Interact)){
                backpack.ReduceItem(interactableItem.data.requiredItem);
                backpack.AddItem(interactableItem.data);
                onInteract(interactableItem.data);
                interactableItem.gameObject.SetActive(false);
                audioManager.PlayCollect();
            }
        } else {
            Debug.Log("Insufficient Items");
        }
    }

    public void SetInteractable(InteractableItem _interactableItem){
        interactableItem = _interactableItem;
        interactButton.interactable = true;
    }
    public void SetUninteractable(){
        interactButton.interactable = false;
        interactableItem = null;
    }
}