using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    Dead
}

public class GameManager : Singleton<GameManager>
{
    public int stageNumber { get; private set; } = 1;

    int backGroundNumber = 0;

    public GameState state = GameState.Intro;

    public float playStartTime;

    public int lives = 3;

    [Header("References")]
    public GameObject introUI;
    public GameObject deadUI;
    public GameObject enemySpawner;
    public GameObject foodSpawner;
    public GameObject goldenSpawner;

    public Player playerScript;

    public TMP_Text scoreText;

    [Header("Stage")]
    public Color[] stageColors;
    public Color[] cameraBackgroundColors;

    public MeshRenderer[] BackgroundMeshRenderer;

    private Camera mainCamera;

    void Start()
    {
        introUI.SetActive(true);
        deadUI.SetActive(false);

        enemySpawner.SetActive(false);
        foodSpawner.SetActive(false);
        goldenSpawner.SetActive(false);

        scoreText.text = "High Score: " + GetHightScore();

        mainCamera = Camera.main;
        mainCamera.backgroundColor = cameraBackgroundColors[0];
    }
    /// <summary>
    /// 실시간 점수를 계산하여 반환하는 함수입니다.
    /// </summary>
    /// <returns></returns>
    public float CalculateScore()
    {
        return Time.time - playStartTime;
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore");
        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }
    int GetHightScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }

    public float CalculateGameSpeed()
    {
        if (state != GameState.Playing)
        {
            return 5f;
        }
        float speed = 8f + (0.5f * Mathf.Floor(CalculateScore() / 5f));
        float maxSpeed = 30f;
        return Mathf.Min(speed, maxSpeed);
    }

    public int nextStageScore()
    {
        return stageNumber * 60;
    }

    void Update()
    {
        if (state == GameState.Playing)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore());
            if (Mathf.FloorToInt(CalculateScore()) >= nextStageScore())
            {
                stageNumber++;
                backGroundNumber = backGroundNumber < cameraBackgroundColors.Length - 1 ? backGroundNumber + 1 : 0;
                mainCamera.backgroundColor = cameraBackgroundColors[backGroundNumber];
                foreach (var meshRenderer in BackgroundMeshRenderer)
                {
                    meshRenderer.material.color = stageColors[backGroundNumber];
                }
            }
        }
        else if (state == GameState.Dead)
        {
            scoreText.text = "High Score: " + GetHightScore();
        }

        if (state == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            state = GameState.Playing;
            introUI.SetActive(false);

            enemySpawner.SetActive(true);
            foodSpawner.SetActive(true);
            goldenSpawner.SetActive(true);

            playStartTime = Time.time;
        }

        if (state == GameState.Playing && lives == 0)
        {
            playerScript.KillPlayer();
            enemySpawner.SetActive(false);
            foodSpawner.SetActive(false);
            goldenSpawner.SetActive(false);
            deadUI.SetActive(true);
            state = GameState.Dead;
            SaveHighScore();
        }

        if (state == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
    }
}
