using UnityEngine;
using VInspector;

public class MinigameManager : MonoBehaviour
{
    public enum GameType
    {
        MainGame,
        ShootEmUp,
        GAME_COUNT
    }

    [SerializeField] Animator transitionAnimator;
    [SerializeField] GameObject mainGame;
    [SerializeField] GameObject shootEmUp;
    [SerializeField] ShootEmUp_EnemySpawner shootEmUp_EnemySpawner;

    [SerializeField] [ReadOnly]
    GameType currGame;

    readonly int animId_transition = Animator.StringToHash("MinigameTransition");
    //private void Update()
    //{
    //    if (GameManager.InputManager.IsInteracting)
    //    {
    //        GameType nextGame = (GameType)(((int)currGame + 1) % (int)GameType.GAME_COUNT);
    //        OnTransitionMinigame(nextGame);
    //    }
    //}
    public void OnTransitionMinigame(GameType type)
    {
        switch (type)
        {
            case GameType.MainGame:
            {
                mainGame.SetActive(true);
                shootEmUp.SetActive(false);
            }
            break;
            case GameType.ShootEmUp:
            {
                mainGame.SetActive(false);
                shootEmUp.SetActive(true);
                shootEmUp_EnemySpawner.SpawnEnemies();

                transitionAnimator.gameObject.SetActive(true);
                transitionAnimator.Play(animId_transition, -1, 0); 
            }
            break;
        }

        currGame = type;
    }
}
