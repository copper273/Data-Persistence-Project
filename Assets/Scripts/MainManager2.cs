// This script is attached to an
// empty GameObject in the menu Scene
// It's where we save all the information that we want the game to remember


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MainManager2 : MonoBehaviour
{
    public static MainManager2 Instance; // This allows us to access this code in any other script

    public string playerName; // For the current player's name
    public int playerScore; // For the player's current score

    public string playerHighScoreName; // For the name of whoever got the last high score
    public int playerHighScore; // For the last high score


    private void Awake()
    {
        // This makes sure there's only ever one instance of the MainManager GameObject
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Makes sure the MainManager GameObject doesn't get destroyed when switching Scenes

        LoadNameAndScore(); // Loads the high score and the name
    }




    [System.Serializable] // Eveything past this point is what gets saved and loaded
    class SaveData
    {
        public string playerHighScoreName;
        public int playerHighScore;
    }



    //  ------------  SAVE & LOAD  ------------
    public void SaveNameAndScore() // Saves the info to a JSON file
    {
        SaveData data = new SaveData();

        data.playerHighScoreName = playerHighScoreName;
        data.playerHighScore = playerHighScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadNameAndScore() // Finds the JSON file, reads it, and converts it back into code
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerHighScoreName = data.playerHighScoreName;
            playerHighScore = data.playerHighScore;
        }
    }
}