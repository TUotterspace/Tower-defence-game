using UnityEngine;

public class GameStats : MonoBehaviour
{
    public static GameStats Instance;

    public int enemiesKilled = 0;
    public int creditsGenerated = 0;
    private float startTime;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            startTime = Time.time;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public float GetPlayTime()
    {
        return Time.time - startTime;
    }

    public void AddKill()
    {
        enemiesKilled++;
    }

    public void AddCredits(int amount)
    {
        creditsGenerated += amount;
    }
}