using UnityEngine;

[CreateAssetMenu(fileName = "NewLevelData", menuName = "Custom/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public string levelName;
    public string sceneName;
    public string levelDescription;
}