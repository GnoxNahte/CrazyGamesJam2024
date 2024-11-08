using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VInspector;

public class GameManager : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    [SerializeField] ShootEmUp_Mananger shootEmUp_Mananger;
    [SerializeField] MainGameManager mainGameManager;
    [SerializeField] MinigameManager minigameManager;
    [SerializeField] EndScreen endScreen;
    [SerializeField] ScoreManager scoreManager;

    static public InputManager InputManager => instance.inputManager;
    static public MainGameManager MainGameManager => instance.mainGameManager;
    static public ShootEmUp_Mananger ShootEmUp_Mananger => instance.shootEmUp_Mananger;
    static public MinigameManager MinigameManager => instance.minigameManager;
    static public EndScreen EndScreen => instance.endScreen;
    static public ScoreManager ScoreManager => instance.scoreManager;

    // Singleton
    //[ShowInInspector]
    public static GameManager instance { get; private set; }

    public void RestartGame()
    {
        // TODO

        // Use this if no other way / no time.
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnPauseGame()
    {

    }

    public void OnLevelUp()
    {

    }

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

        //PersistentManager.Load();
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;

    }

    private void OnSceneChanged(Scene prevScene, Scene nextScene)
    {
        // TODO. Destroy curr obj?
    }
}