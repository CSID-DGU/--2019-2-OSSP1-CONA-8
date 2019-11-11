using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevelOnClick : MonoBehaviour
{

    public void LoadScene(int level)
    {
        Application.LoadLevel(level);
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}