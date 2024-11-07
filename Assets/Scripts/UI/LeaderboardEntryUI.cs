using TMPro;
using Unity.Services.Leaderboards.Models;
using UnityEngine;

public class LeaderboardEntryUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI rankText;
    [SerializeField] TextMeshProUGUI playerNameText;
    [SerializeField] TextMeshProUGUI scoreText;

    public void Init(LeaderboardEntry entry)
    {
        rankText.text = entry.Rank.ToString();
        playerNameText.text = entry.PlayerName;
        scoreText.text = entry.Score.ToString("{0:n0}");
    }
}
