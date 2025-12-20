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
        PrepareMainMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PrepareMainMenu()
    {
        if (GameManager.Instance.ANewGame())
        {
            levelText.text = "New Game Level : 1";
        }
        else
        {
            levelText.text = "Level : " + GameManager.Instance.Level();

            startButton.gameObject.SetActive(false);
            resumeButton.gameObject.SetActive(true);
            clearHistoryButton.gameObject.SetActive(true);
        }
    }
}
