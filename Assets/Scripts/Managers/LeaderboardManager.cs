using Newtonsoft.Json;
using System.Threading.Tasks;
using TMPro;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Leaderboards;
using Unity.Services.Leaderboards.Models;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using VInspector;

public class LeaderboardManager : MonoBehaviour
{
    const string LeaderboardId = "main";
    
    [SerializeField] TMP_InputField playerName;

    [SerializeField] GameObject leaderboardEntryPrefab;
    [SerializeField] GameObject leaderboardUI;
    [SerializeField] RectTransform leaderboardLayout;

    async void Awake()
    {
        await UnityServices.InitializeAsync();

        await SignInAnonymously();
    }

    private void Update()
    {
        if (GameManager.InputManager.Restart)
        {
            bool isLeaderboardActive = !leaderboardUI.activeSelf;
            leaderboardUI.SetActive(isLeaderboardActive);

            if (isLeaderboardActive)
                UpdateLeaderboard();
        }
    }

    async Task SignInAnonymously()
    {
        AuthenticationService.Instance.SignedIn += () => Debug.Log("Signed in as: " + AuthenticationService.Instance.PlayerId);
        AuthenticationService.Instance.SignInFailed += s => Debug.Log(s);

        await AuthenticationService.Instance.SignInAnonymouslyAsync();
    }

    public async void SetPlayerName()
    {
        print("Setting player name to '" + playerName.text + "'");
        await AuthenticationService.Instance.UpdatePlayerNameAsync(playerName.text);
        print("Done setting player name!");
    }

    [Button]
    public async void AddScore()
    {
        var scoreResponse = await LeaderboardsService.Instance.AddPlayerScoreAsync(LeaderboardId, Random.Range(0, 100));
        Debug.Log(JsonConvert.SerializeObject(scoreResponse));
    }

    [Button]
    public async void UpdateLeaderboard()
{
        var scoresResponse =
                await LeaderboardsService.Instance.GetScoresAsync(LeaderboardId);
        Debug.Log(JsonConvert.SerializeObject(scoresResponse));

        foreach (Transform child in leaderboardLayout.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (LeaderboardEntry entry in scoresResponse.Results)
        {
            GameObject leaderboardEntryUI = Instantiate(leaderboardEntryPrefab, leaderboardLayout);
            leaderboardEntryUI.GetComponent<LeaderboardEntryUI>().Init(entry);
        }
    }
}
