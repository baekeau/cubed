using UnityEngine;
using System.Collections;

public class TriggerLoadScene : MonoBehaviour
{
    public float triggerDelay = 2f;

    private bool isPlayerInside = false;
    private float playerStayTime = 0f;

    public LevelDatabase levelDatabase;
    public string levelName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = false;
            playerStayTime = 0f;
        }
    }

    private void Update()
    {
        if (isPlayerInside)
        {
            playerStayTime += Time.deltaTime;

            if (playerStayTime >= triggerDelay)
            {
                isPlayerInside = false;
                LoadLevel();
            }
        }
    }
    
    private void LoadLevel()
    {
        LevelData levelData = levelDatabase.GetLevelDataByName(levelName);
        if (levelData != null)
        {
            GameManager.Instance.LoadScene(levelData.sceneName);
        }
        else
        {
            Debug.LogError($"Level data not found for: {levelName}");
        }
    }
}