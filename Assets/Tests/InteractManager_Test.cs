using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class InteractManager_Test
{
    private GameObject interactManagerObject;
    private InteractManager interactManager;
    private GameObject buttonObject;
    private Button interactButton;
    private GameObject backpackObject;
    private Backpack backpack;
    private InteractableItem interactableItem;
    private ItemData itemData;

    [SetUp]
    public void SetUp()
    {
        interactManagerObject = new GameObject();
        interactManager = interactManagerObject.AddComponent<InteractManager>();

        buttonObject = new GameObject();
        interactButton = buttonObject.AddComponent<Button>();
        interactManager.interactButton = interactButton;

        backpackObject = new GameObject();
        backpack = backpackObject.AddComponent<Backpack>();
        backpack.uiPrefab = (new GameObject());
        interactManager.backpack = backpack;

        interactManager.minigameManager = null;

        interactableItem = new GameObject().AddComponent<InteractableItem>();
        itemData = ScriptableObject.CreateInstance<ItemData>();
        itemData.itemName = "item-1";
        itemData.collectable = true;
        itemData.requiredItem = null;
        interactableItem.data = itemData;
        interactManager.interactableItem = interactableItem;

        interactManager.Init(_ => {});
    }

    [TearDown]
    public void TearDown()
    {
        UnityEngine.Object.DestroyImmediate(interactManagerObject);
        UnityEngine.Object.DestroyImmediate(buttonObject);
        UnityEngine.Object.DestroyImmediate(backpackObject);
        UnityEngine.Object.DestroyImmediate(interactableItem.gameObject);
        UnityEngine.Object.DestroyImmediate(itemData);
    }

    [Test]
    public void Interact_WhenItemRequiresNothing_PerformsInteraction()
    {
        // Arrange
        backpack.items = new Dictionary<string, Backpack.ItemOnBackpack>();

        // Act
        interactManager.SetInteractable(interactableItem);
        interactManager.Interact();

        // Assert
        Assert.AreEqual(backpack.items[interactableItem.data.itemName].total,1);
    }

    [Test]
    public void SetInteractable_SetsInteractableItemAndButton()
    {
        // Arrange
        interactManager.interactButton.interactable = false;
        interactManager.interactableItem = null;

        // Act
        interactManager.SetInteractable(interactableItem);

        // Assert
        Assert.IsNotNull(interactManager.interactableItem);
        Assert.IsTrue(interactButton.interactable);
    }

    [Test]
    public void SetUninteractable_ClearsInteractableItemAndDisablesButton()
    {
        // Arrange
        interactManager.interactButton.interactable = false;
        interactManager.interactableItem = interactableItem;

        // Act
        interactManager.SetUninteractable();

        // Assert
        Assert.IsNull(interactManager.interactableItem);
        Assert.IsFalse(interactButton.interactable);
    }
}
