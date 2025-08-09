using UnityEngine;
using TMPro;
using System.IO;
using System.Collections;
using System.Collections.Generic;

[DefaultExecutionOrder(1000)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Keep these static if you want global access, but it's optional.
    public static string[] scoreTable = new string[10];
    public static string userName;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadGame();
        EnsureTable();
    }

    public void SetUserName(string name)
    {
    userName = string.IsNullOrWhiteSpace(name) ? "Player" : name;
    SaveGame(); // persist immediately
    }

    private void EnsureTable()
    {
    if (scoreTable == null || scoreTable.Length != 10)
        scoreTable = new string[10];

    for (int k = 0; k < scoreTable.Length; k++)
        scoreTable[k] ??= ""; // replace null with empty string
    }

    /// <summary>
    /// Call this to insert a new score and save.
    /// </summary>
    public void SubmitScore(int currentScore)
    {
        UpdateScore(currentScore);
        SaveGame();
    }

    // ------------  SCORE EDITING  ------------
    public void UpdateScore(int currentScore)
    {
        int i = 1; // even index = name, odd index = score
        int oldScore;
        string currentUser = userName;
        string oldUser;

        // Ensure table is initialized
        if (scoreTable == null || scoreTable.Length != 10)
            scoreTable = new string[10];

        while (i < 10)
        {
            // Parse existing score at this slot (odd index)
            if (!int.TryParse(scoreTable[i], out oldScore))
                oldScore = 0;

            // If new score is higher, insert (and push the old pair down)
            if (currentScore > oldScore)
            {
                oldUser = scoreTable[i - 1];             // save old user
                scoreTable[i - 1] = currentUser;          // write new user
                scoreTable[i]     = currentScore.ToString(); // write new score

                // Shift for next iteration (bubble down)
                currentScore = oldScore;
                currentUser  = oldUser;
            }

            i += 2; // next (name,score) pair
        }
    }

    // ------------  SAVE DATA MODEL  ------------
    [System.Serializable]
    public class SaveData
    {
        public string userName;
        public string[] scoreTable;
    }

    // ------------  SAVE / LOAD  ------------
    public void SaveGame()
    {
        // Ensure table isn't null
        if (scoreTable == null || scoreTable.Length != 10)
            scoreTable = new string[10];

        SaveData data = new SaveData
        {
            userName = userName,
            scoreTable = scoreTable
        };

        string json = JsonUtility.ToJson(data);
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");
        File.WriteAllText(path, json);
        // Debug.Log($"Saved to {path}");
    }

    public void LoadGame()
    {
        string path = Path.Combine(Application.persistentDataPath, "savefile.json");

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            userName = data.userName;
            scoreTable = (data.scoreTable != null && data.scoreTable.Length == 10)
                ? data.scoreTable
                : new string[10];
        }
        else
        {
            // First run: initialize defaults
            userName = string.IsNullOrEmpty(userName) ? "Player" : userName;
            scoreTable = new string[10];
        }
    }
}
