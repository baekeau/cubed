using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class GameManager : SerializedMonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject Player { get; private set; }

    [SerializeField] private GameObject _playerPrefab;
    [SerializeField] private Scene _originalScene;

    public SaveData saveData;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Player = Instantiate(_playerPrefab, GetSpawnPosition(), quaternion.identity);
    }

    private Vector3 GetSpawnPosition()
    {
        // Implement logic to determine the spawn position based on the current scene
        return new Vector3(0, 2, -6.5f);
    }
    
    // Scenes
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        // Save game before loading a new scene
        SaveGame();
        
        _originalScene = SceneManager.GetActiveScene();
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!asyncLoad.isDone)
        {
            Debug.Log("load done");
            yield return null;
        }

        SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        // Move the spawnedCharacter to the new scene
        if (Player != null)
        {
            SceneManager.MoveGameObjectToScene(Player, SceneManager.GetActiveScene());
        }
        else
        {
            // If spawnedCharacter is null, instantiate it in the new scene
            Player = Instantiate(_playerPrefab, GetSpawnPosition(), Quaternion.identity);
            Debug.Log("Spawned new character");
        }

        // _mainCamera.tag = "MainCamera";

        yield return UnloadOriginalScene();
    }

    private IEnumerator UnloadOriginalScene()
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(_originalScene);
        while (!asyncUnload.isDone)
        {
            Debug.Log("unload done");
            yield return null;
        }
    }
    
    // Save/load
    [Button]
    private void SaveGame()
    {
        saveData.playerPosition = Player.transform.position; // collect data to save
        // add other data ..
        
        // serialize the saveData object
        byte[] bytes = SerializationUtility.SerializeValue(saveData, DataFormat.JSON);
        
        // save the serialized data to a file
        System.IO.File.WriteAllBytes(GetSaveFilePath(), bytes);
    }
    
    [Button]
    public void LoadGame()
    {
        // Check if the save file exists
        if (System.IO.File.Exists(GetSaveFilePath()))
        {
            // Load the serialized data from the file
            byte[] bytes = System.IO.File.ReadAllBytes(GetSaveFilePath());

            // Deserialize the data
            saveData = SerializationUtility.DeserializeValue<SaveData>(bytes, DataFormat.JSON);

            // Apply the loaded data
            Player.transform.position = saveData.playerPosition;
            // Apply other loaded data
        }
    }
    private string GetSaveFilePath()
    {
        return Application.persistentDataPath + "/savefile.json";
    }


}