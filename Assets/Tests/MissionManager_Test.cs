using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class MissionManager_Test
{
    private GameObject missionManagerObject;
    private MissionManager missionManager;
    private MissionData[] missionDatas;

    [SetUp]
    public void SetUp()
    {
        missionManagerObject = new GameObject();
        missionManager = missionManagerObject.AddComponent<MissionManager>();

        missionDatas = new MissionData[2];
        for (int i = 0; i < missionDatas.Length; i++)
        {
            missionDatas[i] = ScriptableObject.CreateInstance<MissionData>();
            missionDatas[i].missionName = "Mission" + i;
            missionDatas[i].missionSprite = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), Vector2.zero);
            missionDatas[i].itemData = ScriptableObject.CreateInstance<ItemData>();
            missionDatas[i].itemData.itemName = "Item" + i;
        }

        missionManager.missionUIPrefab = new GameObject();
        missionManager.missionUIPrefab.AddComponent<Image>();
        
        var missionUIPrefabChild =  new GameObject();
        missionUIPrefabChild.transform.SetParent(missionManager.missionUIPrefab.transform);
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(missionManagerObject);
        foreach (var missionData in missionDatas)
        {
            UnityEngine.Object.DestroyImmediate(missionData.itemData);
            UnityEngine.Object.DestroyImmediate(missionData);
        }
    }

    [Test]
    public void Init_SetsUpMissionsCorrectly()
    {
        // Act
        missionManager.activeMissions = new Dictionary<string, MissionManager.ActiveMission>();
        missionManager.Init(null, missionDatas);

        // Assert
        Assert.AreEqual(missionDatas.Length, missionManager.activeMissions.Count);
        for (int i = 0; i < missionDatas.Length; i++)
        {
            Assert.IsTrue(missionManager.activeMissions.ContainsKey("Mission" + i));
            Assert.IsFalse(missionManager.activeMissions["Mission" + i].isCompleted);
            Assert.IsNotNull(missionManager.activeMissions["Mission" + i].ui);
        }

    }

    [Test]
    public void CheckMission_CompletesMissionIfItemDataMatches()
    {
        bool completedCallbackInvoked = false;

        // Arrange
        missionManager.activeMissions = new Dictionary<string, MissionManager.ActiveMission>();
        missionManager.Init(() => completedCallbackInvoked = true, missionDatas);

        // Act
        missionManager.CheckMission(missionDatas[0].itemData);

        // Assert
        Assert.IsTrue(missionManager.activeMissions["Mission0"].isCompleted);
        Assert.IsTrue(missionManager.activeMissions["Mission0"].ui.transform.GetChild(0).gameObject.activeSelf);
        Assert.IsFalse(completedCallbackInvoked); 
    }

    [Test]
    public void CheckMission_InvokesCompletedCallbackIfAllMissionsCompleted()
    {
        bool completedCallbackInvoked = false;

        // Arrange
        missionManager.activeMissions = new Dictionary<string, MissionManager.ActiveMission>();
        missionManager.Init(() => completedCallbackInvoked = true, missionDatas);

        // Act
        for (int i = 0; i < missionDatas.Length; i++)
        {
            missionManager.CheckMission(missionDatas[i].itemData);
        }
 
        // Assert
        for (int i = 0; i < missionDatas.Length; i++)
        {
            Assert.IsTrue(missionManager.activeMissions["Mission"+i].isCompleted);
        }
        Assert.IsTrue(completedCallbackInvoked);
    }
}
