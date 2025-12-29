using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Static reference to the singleton instance

    string saveFilePath;
    string gameSaveFileName = "gamesave.json";

    int currentLevel;
    bool isANewGame = true;
    bool isPlayerAlive = true;
    public bool savedGameWasDeleted = false;
    bool gameIsActive = false;
    int playerHealth = 5;
    int food = 20;
    int live = 2;

    //dog instance count according to arrangement in array
    // dog[3] position in array is for intelligent dog
    int[] dogCounts = { 1, 3, 5, 1 };

    float boundary = 48f;

    public int GetLevel() { return currentLevel; }
    public bool GetIsANewGame() { return isANewGame; }
    public bool GetIsPlayerAlive() { return isPlayerAlive; }
    public int GetPlayerHealth() { return playerHealth; }

    public int GetFood() { return food; }
    public int GetLive() { return live; }
    public int[] GetDogCounts() { return dogCounts; }

    public void ReduceFood() { food--; }
    public float GetBounary() { return boundary; }


    public void SetPlayerHealth(int healt)
    {
        if (healt >= 0)
        {
            playerHealth = healt;
        }
    }
    public void SetIsPlayerAlive(bool value) { isPlayerAlive = value; }

    //hold position of game objects
    // vector3 for players position
    //vector3 list for other objects
    Vector3 savedPlayerPosition;
    List<Vector3> savedFoodPositions;
    List<Vector3> savedLivePositions;
    List<Vector3> savedDog1Positions;
    List<Vector3> savedDog2Positions;
    List<Vector3> savedDog3Positions;
    List<Vector3> savedDog4Positions;


    public Vector3 GetSavedPlayerPos() { return savedPlayerPosition; }
    public List<Vector3> GetSavedFoodPos() { return savedFoodPositions; }
    public List<Vector3> GetSavedLivePos() { return savedLivePositions; }
    public List<Vector3> GetSavedDog1Pos() { return savedDog1Positions; }
    public List<Vector3> GetSavedDog2Pos() { return savedDog2Positions; }
    public List<Vector3> GetSavedDog3Pos() { return savedDog3Positions; }
    public List<Vector3> GetSavedDog4Pos() { return savedDog4Positions; }

    void Awake()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, gameSaveFileName);
        // Check if an instance already exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        SaveData savedData = LoadSavedGame();
        if (savedData != null)
        {
            isANewGame = false;

            currentLevel = savedData.currentLevel;
            playerHealth = savedData.playerHealth;
            food = savedData.food;
            live = savedData.live;

            savedPlayerPosition = savedData.savedPlayerPosition;
            savedFoodPositions = savedData.foodPositions;
            savedLivePositions = savedData.livePositions;
            savedDog1Positions = savedData.dog1Positions;
            savedDog2Positions = savedData.dog2Positions;
            savedDog3Positions = savedData.dog3Positions;
            savedDog4Positions = savedData.dog4Positions;
        }

    }

    public void StartGame()
    {
        if (InGameMenu.Instance.gamePaused)
        {
            InGameMenu.Instance.ReturnToGame();
            return;
        }

        if (isANewGame)
        {
            currentLevel = 1;
        }
        SceneManager.LoadScene(currentLevel);
        gameIsActive = true;
        MainMenuManager.Instance.mainMenuPanel.SetActive(false);
        InGameMenu.Instance.inGameMenuPanel.SetActive(true);
        GameStatsManager.Instance.gameStatsPanel.SetActive(true);
    }

    public void ExitGame()
    {
        if (!isPlayerAlive)
        {
            ClearSaveFile();
        }

        SaveGame(DataToSave());

        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

    }


    public void Restart()
    {
        InGameMenu.Instance.gameOverPanel.SetActive(false);
        ClearSaveFile();

        gameIsActive = true;
        currentLevel = 1;
        SceneManager.LoadScene(currentLevel);
    }

    void DefaultStat(int level)
    {
        isPlayerAlive = true;
        playerHealth = 5;
        food = 20;
        live = 2;
    }

    SaveData DataToSave()
    {
        SaveData saveData = new(
            currentLevel,
            playerHealth,
            food,
            live,
            GetGameObjectVector3("Player"),
            GetGameObjectsLocations("Food"),
            GetGameObjectsLocations("Live"),
            GetGameObjectsLocations("Dog1"),
            GetGameObjectsLocations("Dog2"),
            GetGameObjectsLocations("Dog3"),
            GetGameObjectsLocations("Dog4")
        );

        return saveData;
    }

    public void SaveGame(SaveData data)
    {
        if (gameIsActive)
        {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(saveFilePath, json);
        }

    }

    public SaveData LoadSavedGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);

            SaveData data = JsonUtility.FromJson<SaveData>(json);
            return data;
        }
        else
        {
            Debug.LogWarning("save file not found");
            return null;
        }
    }

    public void ClearSaveFile()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }

        DefaultStat(1);
        savedGameWasDeleted = true;
        gameIsActive = false;
        isANewGame = true;
    }

    List<Vector3> GetGameObjectsLocations(string tag)
    {
        List<Vector3> positions = new();
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);

        foreach (GameObject item in gameObjects)
        {
            positions.Add(item.transform.position);
        }
        return positions;
    }

    Vector3 GetGameObjectVector3(string tag)
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag(tag);
        if (gameObject != null)
        {
            return gameObject.transform.position;
        }

        return new Vector3();
    }


    [Serializable]
    public class SaveData
    {
        public int currentLevel;
        public int playerHealth;
        public int food;
        public int live;

        public Vector3 savedPlayerPosition;
        public List<Vector3> foodPositions;
        public List<Vector3> livePositions;
        public List<Vector3> dog1Positions;
        public List<Vector3> dog2Positions;
        public List<Vector3> dog3Positions;
        public List<Vector3> dog4Positions;

        public SaveData(
                int currentLevel,
                int playerHealth,
                int food,
                int live,
                Vector3 savedPlayerPosition,
                List<Vector3> foodPositions,
                List<Vector3> livePositions,
                List<Vector3> dog1Positions,
                List<Vector3> dog2Positions,
                List<Vector3> dog3Positions,
                List<Vector3> dog4Positions
            )
        {

            this.currentLevel = currentLevel;
            this.playerHealth = playerHealth;
            this.food = food;
            this.live = live;

            this.savedPlayerPosition = savedPlayerPosition;
            this.foodPositions = foodPositions;
            this.livePositions = livePositions;
            this.dog1Positions = dog1Positions;
            this.dog2Positions = dog2Positions;
            this.dog3Positions = dog3Positions;
            this.dog4Positions = dog4Positions;
        }
    }

    void OnApplicationQuit()
    {
        if (!isPlayerAlive)
        {
            ClearSaveFile();
        }
        SaveGame(DataToSave());
    }

}