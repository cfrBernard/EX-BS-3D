using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Game Settings")]
    public float gameDuration = 60f; 
    private float timer;
    private bool isGameOver = false;

    [Header("UI Elements")]
    public GameObject gameHUD;
    public TextMeshProUGUI timerText;
    public Image timerFillImage;
    public GameObject endGamePanel;
    public TextMeshProUGUI endGameScoreText;
    public TextMeshProUGUI endGameThrowCountText;
    public TextMeshProUGUI endGameAccuracyText;

    [Header("Audio")]
    public AudioClip gameStartClip;
    public AudioClip timerWarningClip;
    private AudioSource audioSource;
    private bool hasPlayedTimerSound = false;

    private int score = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    public void StartGame()
    {
        
        timer = gameDuration;
        isGameOver = false;
        score = 0;

        if (gameHUD != null)
            gameHUD.SetActive(true);

        if (gameStartClip != null) audioSource.PlayOneShot(gameStartClip);
    }

    void Update()
    {
        if (isGameOver || !gameHUD.activeSelf) return;

        // Timer logic
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
            EndGame();
        }

        if (timer <= 10f && !hasPlayedTimerSound)
        {
            if (timerWarningClip != null)
                audioSource.PlayOneShot(timerWarningClip);

            hasPlayedTimerSound = true;
        }

        UpdateUI();

        // Restart 
        if (Input.GetKeyDown(KeyCode.F) && isGameOver)
        {
            ResetGame();
        }
    }

    public void AddScore(int points)
    {
        if (isGameOver) return;
        
        score += points;
        UpdateUI();
    }

    void EndGame()
    {
        isGameOver = true;
        Debug.Log("GAME OVER!");
        gameHUD.SetActive(false);

        // Show the end game panel
        if (endGamePanel != null)
            endGamePanel.SetActive(true);

        // Show stats
        if (endGameScoreText != null)
            endGameScoreText.text = "" + ScoreManager.Instance.GetScore();
        if (endGameThrowCountText != null)
            endGameThrowCountText.text = "" + ScoreManager.Instance.GetThrowCount();
        if (endGameAccuracyText != null)
            endGameAccuracyText.text = "" + ScoreManager.Instance.GetAccuracy().ToString("F0") + "%";

        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        ScoreManager.Instance.ResetStats();
        
        ResetGame();

        if (endGamePanel != null)
        {
            endGamePanel.SetActive(false);
        }

        Time.timeScale = 1;
        isGameOver = false;

        // UI Reset
        timerText.text = "" + Mathf.Ceil(gameDuration);
    }

    void ResetGame()
    {
        if (gameStartClip != null) audioSource.PlayOneShot(gameStartClip);
        hasPlayedTimerSound = false; 
        gameHUD.SetActive(true);
        timer = gameDuration;
        score = 0;
        isGameOver = false;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (timerText) timerText.text = Mathf.Ceil(timer) + "";
        if (timerFillImage) timerFillImage.fillAmount = timer / gameDuration;
    }
}
