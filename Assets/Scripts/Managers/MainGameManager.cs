using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] MainGame_SpawnerManager spawnerManager;
    [SerializeField] LeaderboardManager leaderboardManager;
    [SerializeField] Player player;

    public MainGame_SpawnerManager SpawnerManager => spawnerManager;
    public Player Player => player;
}
