using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] TMP_Text levelText;
    [SerializeField] Button startButton;
    [SerializeField] Button resumeButton;
    [SerializeField] Button clearHistoryButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PrepareMainMenu();
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

    void DefaultMenu()
    {
        levelText.text = "Level : 1";

        startButton.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(false);
        clearHistoryButton.gameObject.SetActive(false);
    }

    void ContinueGameMenu()
    {
        levelText.text = "Level : " + GameManager.Instance.GetLevel();

        startButton.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(true);
        clearHistoryButton.gameObject.SetActive(true);
    }
}
