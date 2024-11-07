using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] MainGame_SpawnerManager spawnerManager;
    [SerializeField] LeaderboardManager leaderboardManager;

    public MainGame_SpawnerManager SpawnerManager => spawnerManager;
}
