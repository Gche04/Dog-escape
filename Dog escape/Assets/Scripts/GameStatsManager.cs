using TMPro;
using UnityEngine;

public class GameStatsManager : MonoBehaviour
{
    public static GameStatsManager Instance;

    public GameObject gameStatsPanel;
    [SerializeField] TMP_Text currentLevelText;
    [SerializeField] TMP_Text healtText;
    [SerializeField] TMP_Text itemsRemainingText;

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
        GameStats();
    }

    void GameStats()
    {
        currentLevelText.text = "Level\n" + GameManager.Instance.GetLevel();
        healtText.text = "Health\n" + GameManager.Instance.GetPlayerHealth();
        itemsRemainingText.text = "Items\n" + GameManager.Instance.GetFood();
    }
}
