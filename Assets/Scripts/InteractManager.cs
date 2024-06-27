using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.UI;

public class InteractManager : MonoBehaviour
{
    InteractableItem interactableItem;
    Button interactButton;
    Backpack backpack;
    Action<ItemData> onInteract;
    MinigameManager minigameManager;
    
    void  Awake(){
        interactButton = GetComponent<Button>();
        backpack = FindObjectOfType<Backpack>();
        minigameManager = FindObjectOfType<MinigameManager>();
    }

    public void Init(Action<ItemData> _onInteract){
        onInteract =_onInteract;
    }

    public void Interact(){
        if(backpack.IsAvailable(interactableItem.data)){
            if(minigameManager.CheckForCompletion(interactableItem.data, Interact)){
                backpack.ReduceItem(interactableItem.data);
                backpack.AddItem(interactableItem.data);
                onInteract(interactableItem.data);
                interactableItem.gameObject.SetActive(false);
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