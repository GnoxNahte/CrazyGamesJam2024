using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject bgm;

    private void Start()
    {
        DontDestroyOnLoad(bgm);
    }

    public void OnStartClicked()
    {
        SceneManager.LoadScene("GameScene");
    }
}
