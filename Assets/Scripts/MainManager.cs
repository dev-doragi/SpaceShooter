using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public string firstLevel;

    public void StartGame()
    {
        SceneManager.LoadScene(firstLevel);

        if (!PlayerPrefs.HasKey("HiScore"))
        {
            PlayerPrefs.SetInt("HIScore", 0);
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
