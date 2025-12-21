using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public class RestartMenu : MonoBehaviour
{
    [SerializeField] GameObject gameOverPanel;
    //[SerializeField] Button restartButton;
    [SerializeField] Button pauseButton;

    public void Update()
    {
        if (!GameManager.Instance.GetIsPlayerAlive())
        {
            GameOver();
        }
    }

    public void Exit()
    {
        GameManager.Instance.ExitGame();
    }

    public void GameOver()
    {
        GameManager.Instance.GameOver();
        gameOverPanel.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    public void TryAgain()
    {
        gameOverPanel.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        GameManager.Instance.Restart();
    }
}
