using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int score = 0;
    private int throwCount = 0;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score actuel : " + score);
    }

    public void IncrementThrowCount()
    {
        throwCount++;
    }

    public float GetAccuracy()
    {
        return throwCount == 0 ? 0 : (float)score / throwCount * 100f;
    }

    public int GetThrowCount()
    {
        return throwCount;
    }

    public int GetScore()
    {
        return score;
    }

    public void ResetStats()
    {
        score = 0;
        throwCount = 0;
    }

}

