using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    [SerializeField] PoissonDistribution treeDistribution;

    private void Start()
    {
        treeDistribution.SpawnAll();
    }
}
