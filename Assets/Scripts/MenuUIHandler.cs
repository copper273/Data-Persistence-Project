using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


#if UNITY_EDITOR
using UnityEditor;
#endif


[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public Text userName;
    public InputField inputName;
    public Text HighScore;

    void Start()
    {
        //userName.text = PlayerPrefs.GetString("user_name");
        userName.text = GameManager.userName;
        HighScore.text = $"{GameManager.scoreTable[0]}: {GameManager.scoreTable[1]}";   
    }

    public void UpdateName()
    {
        //PlayerPrefs.SetString("user_name", userName.text);

        userName.text = inputName.text;
        // Debug.Log("value changed");
        Debug.Log(userName.text);
        GameManager.userName = userName.text;
        Debug.Log(GameManager.userName);

    }
    // START AND EXIT GAME
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        // GameManager.Instance.SaveScore();
        GameManager.Instance.SaveGame();

    #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
    #else
        Application.Quit();
    #endif
    }
    


}
