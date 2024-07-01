using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class LevelManagerTests
{
    private GameObject levelManagerObject;
    private LevelManager levelManager;
    private GameObject optionScreen;
    private GameObject gameoverScreen;
    private GameObject levelCompletedScreen;
    private PlayerController playerController;

    [SetUp]
    public void SetUp()
    {
        // Arrange
        levelManagerObject = new GameObject();
        levelManager = levelManagerObject.AddComponent<LevelManager>();

        // Mock necessary dependencies
        optionScreen = new GameObject();
        gameoverScreen = new GameObject();
        levelCompletedScreen = new GameObject();
        playerController = new GameObject().AddComponent<PlayerController>();

        levelManager.optionScreen = optionScreen;
        levelManager.gameoverScreen = gameoverScreen;
        levelManager.levelCompletedScreen = levelCompletedScreen;
        levelManager.playerController = playerController;

        optionScreen.tag = "screen-option";
        gameoverScreen.tag = "screen-gameover";
        levelCompletedScreen.tag = "screen-levelcompleted";
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(levelManagerObject);
        Object.DestroyImmediate(optionScreen);
        Object.DestroyImmediate(gameoverScreen);
        Object.DestroyImmediate(levelCompletedScreen);
        Object.DestroyImmediate(playerController.gameObject);
    }

    [UnityTest]
    public IEnumerator ResumeGame_HidesAllScreensAndResumesTime()
    {
        // Act
        levelManager.ResumeGame();
        yield return null;

        // Assert
        Assert.IsFalse(optionScreen.activeSelf);
        Assert.IsFalse(gameoverScreen.activeSelf);
        Assert.IsFalse(levelCompletedScreen.activeSelf);
        Assert.AreEqual(1, Time.timeScale);
        Assert.IsTrue(playerController.isMoveable);
    }

    [UnityTest]
    public IEnumerator Option_ShowsOptionScreen()
    {
        // Act
        levelManager.Option();
        yield return null;

        // Assert
        Assert.IsTrue(optionScreen.activeSelf);
        Assert.AreEqual(0, Time.timeScale);
        Assert.IsFalse(playerController.isMoveable);
    }

    [UnityTest]
    public IEnumerator Win_ShowsLevelCompletedScreenAndUpdatesLevel()
    {
        // Arrange
        int initialLevel = PlayerPrefs.GetInt("Level", 0);
        int nextLevel = (initialLevel + 1) % (SceneManager.sceneCount - 1);

        // Act
        levelManager.Win();
        yield return null;

        // Assert
        Assert.AreEqual(nextLevel, PlayerPrefs.GetInt("Level"));
        Assert.IsTrue(levelCompletedScreen.activeSelf);
        Assert.AreEqual(0, Time.timeScale);
        Assert.IsFalse(playerController.isMoveable);
    }

    [UnityTest]
    public IEnumerator Lose_ShowsGameOverScreen()
    {
        // Act
        levelManager.Lose();
        yield return null;

        // Assert
        Assert.IsTrue(gameoverScreen.activeSelf);
        Assert.AreEqual(0, Time.timeScale);
        Assert.IsFalse(playerController.isMoveable);
    }
}
