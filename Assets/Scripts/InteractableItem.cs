using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableItem : MonoBehaviour
{
    public ItemData data;
    Animator anim;

    void Awake(){
        anim = GetComponent<Animator>();
        anim.speed = 0; 
    }

    public void Glow(){
        if(anim.speed ==0)
        {
            anim.speed = 1;
        }
    }
}
