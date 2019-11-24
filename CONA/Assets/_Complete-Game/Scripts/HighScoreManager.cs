using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HighScoreManager : MonoBehaviour
{
    public static int Score = 0;
    string highScoreKey = "HighScore";
    public Text highScoreText;

    private int savedScore = 0;

    private void Awake()
    {
        savedScore = PlayerPrefs.GetInt(highScoreKey, 0);
        highScoreText.text = "High Score:" + savedScore.ToString("0");
    }

    private void Update()
    {
        //게임컨트롤러에서 스코어 업데이트 후
        //저장되어 있던 게임스코어 비교
        if(Score > savedScore)
        {
            PlayerPrefs.SetInt(highScoreKey, Score);
            highScoreText.text = "HighScore: " + PlayerPrefs.GetInt(highScoreKey);
        }
    }

    // Use this for initialization
    void Start()
    {
        //하이스코어 데이터 초기화
        //PlayerPrefs.DeleteAll();
        highScoreText.text = "HighScore: " + PlayerPrefs.GetInt(highScoreKey);
    }
}
