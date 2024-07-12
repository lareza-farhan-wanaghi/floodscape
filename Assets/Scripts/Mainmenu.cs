using System.Collections;
using System.Collections.Generic;
using System.Security;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    [HideInInspector] public AudioManager audioManager;
    public GameObject levelScreen;
    public GameObject narasiSCreen;


    void Awake(){
        audioManager = FindObjectOfType<AudioManager>();
        Button level1 = levelScreen.transform.GetChild(0).GetComponent<Button>();
        Button level2 = levelScreen.transform.GetChild(1).GetComponent<Button>();
        Button level3 = levelScreen.transform.GetChild(2).GetComponent<Button>();
        level1.onClick.AddListener(()=>LoadLevel(1));
        if(PlayerPrefs.GetInt("Level 1",0)==1){
            level2.interactable = true;
            level2.onClick.AddListener(()=>LoadLevel(2));
        }
        if(PlayerPrefs.GetInt("Level 2",0)==1){
            level3.interactable = true;
            level3.onClick.AddListener(()=>LoadLevel(3));
        }
    }
    public void StartGame(){
        audioManager.PlayButton();
        levelScreen.SetActive(true);
    }

    public void LoadLevel(int _val){
        audioManager.PlayButton();
        narasiSCreen.SetActive(true);
        RectTransform narasitextrect = narasiSCreen.transform.GetChild(0).GetComponent<RectTransform>();
        Image narasitext = narasiSCreen.transform.GetChild(1).GetComponent<Image>();
        narasitextrect.localScale = Vector2.one * 0.7f;
        narasitextrect.LeanScale(Vector2.one,0.5f).setEaseOutBounce();
        LeanTween.value(narasitext.gameObject,(_time)=>narasitext.fillAmount = _time,1,0,3f).setOnComplete(()=> SceneManager.LoadScene("Level "+_val));
    }
}
