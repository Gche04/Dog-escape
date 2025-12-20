using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Static reference to the singleton instance

    int currentLevel;
    bool isANewGame = true;
    int playerHealth;
    int food = 20;
    int live = 2;

    //dog instance count according to arrangement in array
    // dog[3] position in array is for intelligent dog
    int[] dogCounts = { 1, 3, 5, 1 };

    float boundary = 48f;

    public int Level() { return currentLevel; }
    public bool ANewGame() { return isANewGame; }
    public int FoodCount() { return food; }
    public int LiveCount() { return live; }
    public int[] DogCountArray() { return dogCounts; }

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
        SceneManager.LoadScene(currentLevel);
    }

    public void ExitGame()
    {
        SaveGame();

        Application.Quit(); 
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void SaveGame()
    {
        //savecurrentGame data
    }

    public void ContinueGame()
    {
        //load saved game
    }


}