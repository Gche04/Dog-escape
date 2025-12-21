using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject PausedPanel;
    [SerializeField] Button PauseButton;

    public void Exit()
    {
        GameManager.Instance.ExitGame();
    }

    public void Pause()
    {
        Time.timeScale = 0;
        PausedPanel.SetActive(true);
        PauseButton.gameObject.SetActive(false);
    }

    public void ReturnToGame()
    {
        PausedPanel.SetActive(false);
        PauseButton.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}
