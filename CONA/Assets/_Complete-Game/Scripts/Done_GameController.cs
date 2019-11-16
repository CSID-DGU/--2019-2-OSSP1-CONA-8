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
    public Text restartText;
    public Text gameOverText;

    private bool gameOver;
    private bool restart;
    private int score;

    public bool isCameraMirroredInEditor;

    public Transform ARcameraTransform;
    public MirrorFlipCamera mirrorFlipCameraNonAR;

    public UnityEvent OnGameOver;

    void Start()
    {
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        score = 0;
        UpdateScore();
        StartCoroutine(SpawnWaves());
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

            if (gameOver)
            {
                restartText.text = "Press 'R' for Restart";
                restart = true;
                break;
            }
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
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over!";
        gameOver = true;

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