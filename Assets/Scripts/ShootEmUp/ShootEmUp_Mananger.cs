using UnityEngine;
using VInspector;

public class ShootEmUp_Mananger : MonoBehaviour
{
    [SerializeField] ShootEmUp_Player player;

    public static ShootEmUp_Player Player => instance.player;

    // Singleton
    [ShowInInspector]
    public static ShootEmUp_Mananger instance { get; private set; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            Debug.LogError("More than 1 GameManager. Destroying this. Name: " + name);
            return;
        }
    }
}
