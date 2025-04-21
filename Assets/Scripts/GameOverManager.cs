using UnityEngine;
using TMPro; // For TextMeshPro

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;

    [SerializeField] private GameObject gameOverPanel;

    [Header("Stat Text Fields")]
    public TMP_Text timeSurvivedText;
    public TMP_Text enemiesKilledText;
    public TMP_Text creditsEarnedText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerGameOver()
    {
        // 1. Get data from GameStats
        float playTime = GameStats.Instance.GetPlayTime();
        int minutes = Mathf.FloorToInt(playTime / 60f);
        int seconds = Mathf.FloorToInt(playTime % 60f);

        // 2. Update text fields
        timeSurvivedText.text = $"Time Survived: {minutes:00}:{seconds:00}";
        enemiesKilledText.text = $"Enemies Killed: {GameStats.Instance.enemiesKilled}";
        creditsEarnedText.text = $"Credits Earned: {GameStats.Instance.creditsGenerated}";

        // 3. Show the panel
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}