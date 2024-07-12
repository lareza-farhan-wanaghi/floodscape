using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genangan : MonoBehaviour
{
    void OnEnable(){
        transform.localScale = Vector3.zero;
        transform.LeanScale(Vector3.one,0.5f).setEaseOutExpo();
    }
}
