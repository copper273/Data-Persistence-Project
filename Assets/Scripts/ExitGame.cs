using UnityEditor;
using UnityEngine;

public class ExitGame : MonoBehaviour
{

    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }



    public void SaveBest()
    {
        MainManager2.Instance.SaveNameAndScore();
    }
}
