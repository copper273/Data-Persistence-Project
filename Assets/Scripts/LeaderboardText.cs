using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;

public class LeaderboardText : MonoBehaviour
{
    public TMP_Text leaderboardText;
    [Range(1, 10)] public int maxEntries = 10;

    // Start is called before the first frame update
    void Start()
    {
        if (leaderboardText == null)
        {
            Debug.LogError("LeaderboardText: leaderboardText is not assigned.");
            return;
        }

        var table = GameManager.scoreTable;
        if (table == null || table.Length != 10) { leaderboardText.text = "No scores yet."; return; }

        var sb = new StringBuilder();
        int rank = 1;

        for (int i = 0; i < table.Length && rank <= maxEntries; i += 2)
        {
            string name = string.IsNullOrEmpty(table[i]) ? "-" : table[i];
            string scoreStr = table[i + 1];
            int score = 0;
            int.TryParse(scoreStr, out score);

            // Skip completely empty slots (optional)

            if (string.IsNullOrEmpty(table[i]) && string.IsNullOrEmpty(scoreStr))
                continue;

            // Nicely aligned columns (use a monospaced TMP font for perfect alignment)
            sb.AppendLine($"{rank,2}. {name,-12} {score}");
            rank++;
        }

        leaderboardText.text = sb.Length > 0 ? sb.ToString() : "No scores yet.";
    }

}
