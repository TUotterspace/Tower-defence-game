using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public TMP_Text timeText;
    public TMP_Text killsText;
    public TMP_Text creditsText;

    public void RestartGame()
    {
        SceneManager.LoadScene("YourMainSceneName"); // Replace with your real scene name
    }

    void Start()
    {
        if (GameStats.Instance != null)
        {
            float timePlayed = GameStats.Instance.GetPlayTime();
            int kills = GameStats.Instance.enemiesKilled;
            int credits = GameStats.Instance.creditsGenerated;

            timeText.text = "Time Played: " + Mathf.RoundToInt(timePlayed) + "s";
            killsText.text = "Enemies Killed: " + kills;
            creditsText.text = "Credits Generated: " + credits;
        }
    }
}