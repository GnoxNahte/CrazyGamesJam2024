using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField] MainGame_SpawnerManager spawnerManager;
    public MainGame_SpawnerManager SpawnerManager => spawnerManager;
}