using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mainmenu : MonoBehaviour
{
    [HideInInspector] public AudioManager audioManager;
    public GameObject levelScreen;


    void Awake(){
        audioManager = FindObjectOfType<AudioManager>();
        Button level1 = levelScreen.transform.GetChild(0).GetComponent<Button>();
        Button level2 = levelScreen.transform.GetChild(1).GetComponent<Button>();
        Button level3 = levelScreen.transform.GetChild(2).GetComponent<Button>();
        level1.onClick.AddListener(Level1);
        if(PlayerPrefs.GetInt("Level 1",0)==1){
            level2.interactable = true;
            level2.onClick.AddListener(Level2);
        }
        if(PlayerPrefs.GetInt("Level 2",0)==1){
            level3.interactable = true;
            level3.onClick.AddListener(Level3);
        }
    }
    public void StartGame(){
        audioManager.PlayButton();
        levelScreen.SetActive(true);
    }

    public void Level1(){
        audioManager.PlayButton();
        SceneManager.LoadScene("Level 1");
    }

    public void Level2(){
        audioManager.PlayButton();
        SceneManager.LoadScene("Level 2");
    }
    public void Level3(){
        audioManager.PlayButton();
        SceneManager.LoadScene("Level 3");
    }
}
