using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Backpack_Test
{
    private GameObject backpackObject;
    private Backpack backpack;
    private ItemData collectableItemData;
    private ItemData nonCollectableItemData;
    private ItemData requiredItemData;

    [SetUp]
    public void SetUp()
    {
        backpackObject = new GameObject();
        backpack = backpackObject.AddComponent<Backpack>();
        backpack.uiPrefab = (new GameObject());

        collectableItemData = ScriptableObject.CreateInstance<ItemData>();
        collectableItemData.itemName = "CollectableItem";
        collectableItemData.collectable = true;

        nonCollectableItemData = ScriptableObject.CreateInstance<ItemData>();
        nonCollectableItemData.itemName = "NonCollectableItem";
        nonCollectableItemData.collectable = false;

        requiredItemData = ScriptableObject.CreateInstance<ItemData>();
        requiredItemData.itemName = "RequiredCollectableItem";
        requiredItemData.collectable = false;
        requiredItemData.requiredItem = collectableItemData;
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(backpackObject);
        Object.DestroyImmediate(collectableItemData);
        Object.DestroyImmediate(nonCollectableItemData);
        Object.DestroyImmediate(requiredItemData);
    }

    [Test]
    public void AddItem_CollectableItem_IncreasesItemCount()
    {
        // Arrange
        backpack.items = new Dictionary<string, Backpack.ItemOnBackpack>();
        
        // Act
        backpack.AddItem(collectableItemData);

        // Assert
        Assert.AreEqual(backpack.items[collectableItemData.itemName].total,1);
    }

    [Test]
    public void AddItem_NonCollectableItem_DoesNotAddItem()
    {
        // Arrange
        backpack.items = new Dictionary<string, Backpack.ItemOnBackpack>();

        // Act
        backpack.AddItem(nonCollectableItemData);

        // Assert
        Assert.IsFalse(backpack.items.ContainsKey(nonCollectableItemData.itemName));
    }

    [Test]
    public void ReduceItem_ItemIsNotRemoved_DecreasesItemCount()
    {
        // Arrange
        backpack.items = new Dictionary<string, Backpack.ItemOnBackpack>();
        backpack.AddItem(collectableItemData);
        backpack.AddItem(collectableItemData);

        // Act
        backpack.ReduceItem(collectableItemData);

        // Assert
        Assert.AreEqual(backpack.items[collectableItemData.itemName].total, 1);
    }


    [Test]
    public void IsAvailable_ItemIsAvailable_ReturnsTrueNonRequred()
    {
        // Arrange
        backpack.items = new Dictionary<string, Backpack.ItemOnBackpack>();

        // Act
        bool isAvailable = backpack.IsAvailable(collectableItemData);

        // Assert
        Assert.IsTrue(isAvailable);
    }

    [Test]
    public void IsAvailable_ItemIsNotAvailable_ReturnsFalseRequired()
    {
        // Arrange
        backpack.items = new Dictionary<string, Backpack.ItemOnBackpack>();

        // Act
        bool isAvailable = backpack.IsAvailable(requiredItemData);

        // Assert
        Assert.IsFalse(isAvailable);
    }

        [Test]
    public void IsAvailable_ItemIsNotAvailable_ReturnsTrueRequired()
    {
        // Arrange
        backpack.items = new Dictionary<string, Backpack.ItemOnBackpack>();
        backpack.AddItem(collectableItemData);

        // Act
        bool isAvailable = backpack.IsAvailable(requiredItemData);

        // Assert
        Assert.IsFalse(isAvailable);
    }
}
