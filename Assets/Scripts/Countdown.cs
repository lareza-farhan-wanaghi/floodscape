using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    TextMeshProUGUI text;
    CanvasGroup canvasGroup;
    void Awake(){
        text = GetComponent<TextMeshProUGUI>();
        canvasGroup = GetComponent<CanvasGroup>();
        text.transform.localScale = Vector3.zero;
    }

    public void Show(string _text){
        LeanTween.cancel(text.gameObject);
        text.SetText(_text);
        text.transform.localScale = Vector3.one;
        text.transform.LeanScale(Vector3.zero, 0.5f).setEaseInQuad();
        LeanTween.value(text.gameObject, (_time)=>canvasGroup.alpha =_time,1, 0, 0.5f).setEaseInQuad();
    }
}
