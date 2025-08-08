// This script is where all the score stuff is done

using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score;
    public int highScore;
    public bool gameOver;
    public string highScoreName;


    // Remember to drag the Score text gameObject into the Inpector of the empty GameObject connected to this script
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;


    void Start()
    {
        gameOver = false;

        // Displays the High Score Name and the High Score
        highScoreText.text = "Best score: " + MainManager2.Instance.playerHighScoreName + ": " + MainManager2.Instance.playerHighScore;

        // If no-one's got a high score yet, display "nobody" as the name
        if(MainManager2.Instance.playerHighScoreName == "")
        {
            MainManager2.Instance.playerHighScoreName = "nobody";
        }

    }


    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Mouse0)) // when the left mouse button is clicked, add 1 to the current score
        {
            gameOver = false;
            UpdateScore(1);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1)) // if right mouse button is clicked...
        {
            SetHighScore(); // Set the high score

            gameOver = true;

            ResetScore(); // Set the current score back to 0
        }
    }


    // ------------  SCORE EDITING  ------------
    private void UpdateScore(int scoreToAdd)
    {
        if(gameOver == false)
        {
            score += scoreToAdd; // Adds to current score
            scoreText.text = "Score: " + score; // Updates the score display
            MainManager2.Instance.playerScore = score; // set the playerScore to the new current score
        }
    }
    private void ResetScore()
    {
        score = 0; // Sets the score back to 0
        scoreText.text = "Score: " + score; // Updates the display
    }


    // ------------  SCORE SETTING  --------------

   private void SetHighScore()
    {
        if(score > MainManager2.Instance.playerHighScore) // If the current score is more than the last saved high score...
        {

            highScore = score; // Set the high score to the current score
            highScoreName = MainManager2.Instance.playerName; // Set the high score name to the player's current name


            if(MainManager2.Instance.playerName != MainManager2.Instance.playerHighScoreName) // If the current name isn't the same as the last saved name...
            {
                // Set the saved name to be the same as the current name
                MainManager2.Instance.playerHighScoreName = MainManager2.Instance.playerName;
            }

            if (MainManager2.Instance.playerScore >= MainManager2.Instance.playerHighScore) // If the current score is higher or the same as the saved high score...
            {
                // Set the saved high score to the highScore
                MainManager2.Instance.playerHighScore = highScore;
            }

            // Update the high score display
            highScoreText.text = "Best score: " + MainManager2.Instance.playerHighScoreName + ": " + MainManager2.Instance.playerHighScore;

        }
    }
}
