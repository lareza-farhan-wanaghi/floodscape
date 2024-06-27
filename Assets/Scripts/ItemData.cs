using UnityEngine;

// Create a new ScriptableObject class
[CreateAssetMenu(menuName = "Item Data")]
public class ItemData : ScriptableObject
{
    public Sprite itemSprite;
    public string itemName;
    public ItemData requiredItem;
    public bool collectable;
    public int minigameIndex = -1;
}