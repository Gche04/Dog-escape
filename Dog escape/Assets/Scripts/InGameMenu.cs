using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InGameMenu : MonoBehaviour
{
    public static InGameMenu Instance;

    public bool gamePaused = false;
    public GameObject inGameMenuPanel;
    [SerializeField] Button pauseButton;

    public GameObject gameOverPanel;

    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!GameManager.Instance.GetIsPlayerAlive())
        {
            StartCoroutine(DelayGameOverMenu());
        }
    }

    public void Pause()
    {
        Time.timeScale = 0;
        MainMenuManager.Instance.PausedGameMenu();
        gamePaused = true;
        inGameMenuPanel.SetActive(false);
    }

    public void ReturnToGame()
    {
        gamePaused = false;
        MainMenuManager.Instance.mainMenuPanel.SetActive(false);
        inGameMenuPanel.SetActive(true);
        Time.timeScale = 1;
    }

    IEnumerator DelayGameOverMenu()
    {
        yield return new WaitForSeconds(2);
        GameOverMenu();
    }
    void GameOverMenu()
    {
        gameOverPanel.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }
}
