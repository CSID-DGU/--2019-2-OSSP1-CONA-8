using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HighScoreManager : MonoBehaviour
{

    public static List<int> Score = new List<int>();  //플레이어가 재시작을 했을 시에 대한 score리스트
    string highScoreKey = "HighScore";
    public Text highScoreText;
    private int savedScore = 0;
    public static List<int> HighScoreList = new List<int> { 0 };

    public void HighScoreUpdate()
    {
        HighScoreList = new List<int>();
        for (int j = 0; PlayerPrefs.HasKey(highScoreKey + j); j++)
        {
            HighScoreList.Add(PlayerPrefs.GetInt(highScoreKey + j));
        }

        if (HighScoreList.Count == 0)
        {
            HighScoreList.Add(0);
        }

       for(int i = 0;i<Score.Count; i++)
        {
            foreach (int RecordedValue in HighScoreList)
            {
                if (Score[i] > RecordedValue)
                {
                    HighScoreList.Insert(HighScoreList.IndexOf(RecordedValue), Score[i]);
                    break;
                }
                else if (Score[i] == RecordedValue)
                {
                    break;
                }
            }
        }
        
    }
    private void Awake()
    {
        HighScoreUpdate();
        Score.Clear();

        int i;
        highScoreText.text = "";
        for (i = 0; i < HighScoreList.Count && i < 5; i++)
        {
            if (HighScoreList[i] == 0) break;
            PlayerPrefs.SetInt(highScoreKey + i, HighScoreList[i]);
        }
    }

    // Use this for initialization
    void Start()
    {
        //PlayerPrefs.DeleteAll();
        
        //Awake로 highscore업데이트 후 실행
        //HighScoreText에 ranking표시
        for (int i = 0; i < HighScoreList.Count && i<5; i++)
        {
            if (HighScoreList[i] == 0) break;
            highScoreText.text += (i+1) + ". " + PlayerPrefs.GetInt(highScoreKey + i) + "\n";
        }

        //하이스코어 데이터 초기화
        //PlayerPrefs.DeleteAll();

    }
}
