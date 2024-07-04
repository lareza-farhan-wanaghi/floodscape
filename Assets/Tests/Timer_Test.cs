using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;

public class Timer_Test
{
    private GameObject timerObject;
    private Timer timer;
    private GameObject[] genangans;

    [SetUp]
    public void SetUp()
    {
        timerObject = new GameObject();
        timer = timerObject.AddComponent<Timer>();
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(timerObject);
    }

    [Test]
    public void StartTime_Initial()
    {
        // Arrange
        float maxTime = 3f;
        timer.lastTriggeredTimeIndex = 1;
        timer.maxTime = 2f;

        // Act
        timer.StartTime(null, maxTime);

        // Assert
        Assert.AreEqual(0, timer.lastTriggeredTimeIndex);
        Assert.AreNotEqual(maxTime, timer.maxTime);
    }
}
