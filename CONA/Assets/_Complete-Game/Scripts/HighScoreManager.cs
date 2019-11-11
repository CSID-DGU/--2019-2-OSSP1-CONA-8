using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreManager : MonoBehaviour
{
    string highScoreKey = "HighScore";
    public Text highScoreText;

    // Use this for initialization
    void Start()
    {
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt(highScoreKey);
    }
}
