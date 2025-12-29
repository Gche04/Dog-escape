using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager Instance;

    public GameObject mainMenuPanel;
    [SerializeField] TMP_Text levelText;
    [SerializeField] Button startButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button clearHistoryButton;


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

    // Update is called once per frame
    void Update()
    {
        //handles main menu only game is not paused
        if (!InGameMenu.Instance.gamePaused)
        {
            PrepareMainMenu();
        }
        
    }

    void PrepareMainMenu()
    {
        if (GameManager.Instance.GetIsANewGame() || GameManager.Instance.savedGameWasDeleted)
        {
            DefaultMenu();
        }
        else
        {
            ContinueGameMenu();
        }
    }

    // main menu for new game
    void DefaultMenu()
    {
        levelText.text = "Level : 1";

        startButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        clearHistoryButton.gameObject.SetActive(false);
    }

    // main menu for when its not a new game
    void ContinueGameMenu()
    {
        levelText.text = "Level : " + GameManager.Instance.GetLevel();

        startButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        clearHistoryButton.gameObject.SetActive(true);
    }

    // paused menu
    public void PausedGameMenu()
    {
        mainMenuPanel.SetActive(true);
        levelText.text = "Paused--  Level : " + GameManager.Instance.GetLevel();
        
        startButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        clearHistoryButton.gameObject.SetActive(false);
    }
}
