using TMPro;
using UnityEngine;
using UnityEngine.UI; // or TMPro if using TextMeshPro

public class SurvivalTimer : MonoBehaviour
{
    public TMP_Text timerText; // Use TMP_Text if using TextMeshPro
    private float timeElapsed;

    void Update()
    {
        timeElapsed += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timeElapsed / 60F);
        int seconds = Mathf.FloorToInt(timeElapsed % 60F);

        timerText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
    }
}