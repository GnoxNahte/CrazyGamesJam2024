using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalscore;
    public void ShowEndScreen()
    {
        finalscore.text = GameManager.ScoreManager.GetScore().ToString();
        gameObject.SetActive(true);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
