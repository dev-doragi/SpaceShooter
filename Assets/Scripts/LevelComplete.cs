using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public float timeToLoad;
    public string nextLevel;

    void Update()
    {
        timeToLoad -= Time.deltaTime;
        if (timeToLoad <= 0)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
}
