using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    enum ScreenType {
        OPTION,
        LEVELCOMPLETED,
        GAMEOVER,

    }
    [HideInInspector] public Timer timer;
    [HideInInspector] public PlayerController playerController;
    [HideInInspector] public GameObject optionScreen;
    [HideInInspector] public GameObject gameoverScreen;
    [HideInInspector] public InteractManager interactManager;
    [HideInInspector] public MissionManager missionManager;
    [HideInInspector] public GameObject levelCompletedScreen;
    public MissionData[] levelMissionData;

    void Awake(){
        timer = FindObjectOfType<Timer>();
        playerController = FindObjectOfType<PlayerController>();
        optionScreen = GameObject.FindGameObjectWithTag("screen-option");
        gameoverScreen = GameObject.FindGameObjectWithTag("screen-gameover");
        levelCompletedScreen = GameObject.FindGameObjectWithTag("screen-levelcompleted");
        interactManager = FindObjectOfType<InteractManager>();
        missionManager = FindAnyObjectByType<MissionManager>();
    }

    void Start()
    {
        interactManager.Init(missionManager.CheckMission);
        missionManager.Init(()=>ShowScreen(ScreenType.LEVELCOMPLETED),levelMissionData);
        timer.StartTime(Lose, 15);
        playerController.ResetPosition();
        HideScreen();
    }

    public void RestartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel(){
        SceneManager.LoadScene("Level "+PlayerPrefs.GetInt("Level",0));
    }

    public void ResumeGame(){
        HideScreen();
    }

    public void Mainmenu(){
        SceneManager.LoadScene("Mainmenu");
    }

    public void Option(){
        ShowScreen(ScreenType.OPTION);
    }

    public void Win(){
        PlayerPrefs.SetInt("Level",(PlayerPrefs.GetInt("Level",0)+1)%(SceneManager.sceneCount-1));
        ShowScreen(ScreenType.LEVELCOMPLETED);
    }
     
    public void Lose(){
        ShowScreen(ScreenType.GAMEOVER);
    }

    void HideScreen(){
        optionScreen.SetActive(false);
        levelCompletedScreen.SetActive(false);
        gameoverScreen.SetActive(false);   
        playerController.ToggleIsMoveable(true);
        Time.timeScale = 1;
    }

    void ShowScreen(ScreenType _screenType){
        Time.timeScale = 0;
        playerController.ToggleIsMoveable(false);
        optionScreen.SetActive(false);
        levelCompletedScreen.SetActive(false);
        gameoverScreen.SetActive(false);
        switch(_screenType){
            case ScreenType.OPTION:
                optionScreen.SetActive(true);
                break;
            case ScreenType.LEVELCOMPLETED:
                levelCompletedScreen.SetActive(true);
                break;
            case ScreenType.GAMEOVER:
                gameoverScreen.SetActive(true);
                break;
        }
    }

}
