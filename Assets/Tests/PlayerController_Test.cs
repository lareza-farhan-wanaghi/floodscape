using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.TestTools;

public class PlayerController_Test
{
    private GameObject playerObject;
    private PlayerController playerController;

    [SetUp]
    public void Setup()
    {
        playerObject = new GameObject();
        playerController = playerObject.AddComponent<PlayerController>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(playerObject);
    }

    [Test]
    public void ToggleIsMoveable_Test()
    {
        // Arrange
        bool initialMoveableState = true;
        playerController.ToggleIsMoveable(initialMoveableState);

        // Act
        playerController.ToggleIsMoveable(false);

        // Assert
        Assert.IsFalse(playerController.GetIsMoveable());
    }
}
