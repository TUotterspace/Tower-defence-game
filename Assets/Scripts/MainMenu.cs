using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SampleScene"); // Make sure your scene name matches!
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // This will only show in the editor
        Application.Quit();     // Actually quits the game (only works in build)
    }
}