using UnityEngine;

// Create a new ScriptableObject class
[CreateAssetMenu(menuName = "Mission Data")]
public class MissionData : ScriptableObject
{
    public string missionName;
    public string missionUrgentNarasi;
    public AudioClip clip;
    public ItemData itemData;
    public Sprite missionSprite;
}
