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
    [HideInInspector] public AudioManager audioManager;
    public MissionData[] levelMissionData;

    void Awake(){
        timer = FindObjectOfType<Timer>();
        playerController = FindObjectOfType<PlayerController>();
        optionScreen = GameObject.FindGameObjectWithTag("screen-option");
        gameoverScreen = GameObject.FindGameObjectWithTag("screen-gameover");
        levelCompletedScreen = GameObject.FindGameObjectWithTag("screen-levelcompleted");
        interactManager = FindObjectOfType<InteractManager>();
        missionManager = FindObjectOfType<MissionManager>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        interactManager.Init(missionManager.CheckMission);
        missionManager.Init(Win,levelMissionData);
        timer.StartTime(Lose, 30);
        playerController.ResetPosition();
        HideScreen();
    }

    public void RestartLevel(){
        audioManager.PlayButton();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void NextLevel(){
        audioManager.PlayButton();
        int buildidx = SceneManager.GetActiveScene().buildIndex+1;
        int maxbuild = SceneManager.sceneCountInBuildSettings;
        int nextcount = buildidx % maxbuild ;
        Debug.Log(buildidx);
        Debug.Log(maxbuild);
        Debug.Log(nextcount);
        SceneManager.LoadScene(nextcount);
    }

    public void ResumeGame(){
        audioManager.PlayButton();
        HideScreen();
    }

    public void Mainmenu(){
        audioManager.PlayButton();
        SceneManager.LoadScene("Mainmenu");
    }

    public void Option(){
        audioManager.PlayButton();
        ShowScreen(ScreenType.OPTION);
    }

    public void Win(){
        audioManager.PlayWin();
        PlayerPrefs.SetInt(SceneManager.GetActiveScene().name, 1);
        ShowScreen(ScreenType.LEVELCOMPLETED);
    }
     
    public void Lose(){
        audioManager.PlayLose();
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
