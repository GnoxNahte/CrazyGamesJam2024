using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int EnemiesKilled;
    public int EnemyMultiplier;
    public int timeMultiplier;

    public int GetScore()
    {
        return EnemiesKilled * EnemyMultiplier + (int)(Time.timeSinceLevelLoad * timeMultiplier);
    }
}
