using UnityEngine;

public class Utility
{
    public static Vector2 GetRandomPointOnUnitCircle()
    {
        float randomAngle = Random.Range(0f, Mathf.PI * 2f);
        return new Vector2(Mathf.Sin(randomAngle), Mathf.Cos(randomAngle)).normalized;
    }
}
