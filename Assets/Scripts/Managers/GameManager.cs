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
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;

    private int score = 0;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        ResetGame();
    }

    void Update()
    {
        if (isGameOver) return;

        // Timer logic
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            timer = 0;
            EndGame();
        }

        UpdateUI();

        // Restart 
        if (Input.GetKeyDown(KeyCode.R) && isGameOver)
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
    }

    void ResetGame()
    {
        timer = gameDuration;
        score = 0;
        isGameOver = false;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (timerText) timerText.text = "Time: " + Mathf.Ceil(timer);
        if (scoreText) scoreText.text = "Score: " + score;
    }
}
