using UnityEngine;

[CreateAssetMenu(fileName = "LevelDatabase", menuName = "Custom/LevelDatabase", order = 2)]
public class LevelDatabase : ScriptableObject
{
    public LevelData[] levels;

    public LevelData GetLevelDataByName(string levelName)
    {
        foreach (LevelData levelData in levels)
        {
            if (levelData.levelName == levelName)
            {
                return levelData;
            }
        }
        return null;
    }
}