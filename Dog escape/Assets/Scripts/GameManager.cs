using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set;} // Static reference to the singleton instance

    [SerializeField] SpawnManager spawnManager;
    
    int currentLevel;
    bool isANewGame = true;
    int playerHealth;

    int foodCount;
    int liveCount;

    int dog0Count;
    int dog1Count;
    int dog2Count;
    
    int intelligentDogCount;

    float bounary;


    void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            // If not, set this object as the instance and prevent it from being destroyed
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // If another instance already exists, destroy this new one to enforce the singleton pattern
            Destroy(gameObject);
        }
    }

    public void StartNewGame()
    {
        currentLevel = 1;
        SetPlayerHealth();
        SceneManager.LoadScene(currentLevel);
    }

    public void Save()
    {
        
    }

    public void ContinueGame()
    {
        //load saved game
    }

    void SetPlayerHealth()
    {
        playerHealth += currentLevel + 2;
    }

    
}