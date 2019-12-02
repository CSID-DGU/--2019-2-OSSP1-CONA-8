using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class Done_GameController : MonoBehaviour
{
    public Done_PlayerController PlayerController;

    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public Text scoreText;
    public Text lastScore;
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    public bool isCameraMirroredInEditor;

    public Transform ARcameraTransform;
    public MirrorFlipCamera mirrorFlipCameraNonAR;

    public UnityEvent OnGameOver;

    private float TimeLeft = 10.0f;
    private float nextTime = 0.0f;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());

        InitCameraMirroring();

        for (int i = 0; i < hazardCount; ++i)
        {
            hazards[i].GetComponent<Done_Mover>().speed = -5;
        }
    }

    void Update()
    {
        if (restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        if(Time.time > nextTime)
        {
            nextTime = Time.time + TimeLeft;
            MoveFaster();
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

        }
    }

    //적들 속도를 점점 증가시키는 함수
    void MoveFaster()
    {
        for(int i=0;i< hazardCount;++i)
        {
            hazards[i].GetComponent<Done_Mover>().speed -= 1;
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        lastScore.text = "Score: " + score;
        if(gameOver) HighScoreManager.Score.Add(score);
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;
        //플레이어 사망시 게임 스코어 업데이트
        OnGameOver.Invoke();
        
    }

    private void InitCameraMirroring()
    {
        if (isCameraMirroredInEditor)
        {
            ARcameraTransform.localScale = new Vector3(
                        ARcameraTransform.localScale.x * -1f,
                        ARcameraTransform.localScale.y,
                        ARcameraTransform.localScale.z);

            mirrorFlipCameraNonAR.enabled = true;

            PlayerController.positionFactor *= -1f;
        }
    }

    void InitPlayer()
    {
        PlayerController.enabled = true;
    }

    //재시작 버튼을 누를 때 실행되는 함수
    public void OnClickRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //다시 게임화면 로드
    }

    //게임 종료 버튼을 누를 때 실행되는 함수
    public void OnClickGameExit()
    {
        //Application.LoadLevel(0);
        SceneManager.LoadScene(0); //메인화면으로 간다.
    }
}