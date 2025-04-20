using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance;
    [SerializeField] private GameObject gameOver;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // So it stays alive across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerGameOver()
    {
       gameOver.SetActive(true);
    }
}