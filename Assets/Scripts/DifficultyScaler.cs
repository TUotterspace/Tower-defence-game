using UnityEngine;

public static class DifficultyScaler
{
    public static float GetDifficultyMultiplier()
    {
        float time = GameStats.Instance.GetPlayTime();
        return 1f + (time / 60f); // Every 60 seconds increases difficulty by 1x
    }
}