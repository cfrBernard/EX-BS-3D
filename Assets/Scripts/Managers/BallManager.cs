using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;  
    public int maxBalls = 10; 

    private Queue<GameObject> activeBalls = new Queue<GameObject>();
    private List<GameObject> allBalls = new List<GameObject>();

    void Start()
    {
        // Initialize the ball pool
        for (int i = 0; i < maxBalls; i++)
        {
            GameObject newBall = Instantiate(ballPrefab);
            newBall.SetActive(false);
            allBalls.Add(newBall);
        }
    }

    // Retrieve a ball from the pool
    public GameObject GetBall()
    {
        GameObject ballToReturn = null;

        // Retrieve an inactive ball
        if (activeBalls.Count < maxBalls)
        {
            ballToReturn = GetInactiveBall();
        }
        else
        {
            // Deactivate the oldest one
            ballToReturn = activeBalls.Dequeue();
            ballToReturn.SetActive(false);    
        }

        ballToReturn.SetActive(true);
        ballToReturn.transform.position = spawnPoint.position;
        
        Rigidbody rb = ballToReturn.GetComponent<Rigidbody>();
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.useGravity = true;

        activeBalls.Enqueue(ballToReturn);

        return ballToReturn;
    }

    private GameObject GetInactiveBall()
    {
        foreach (GameObject ball in allBalls)
        {
            if (!ball.activeSelf)
            {
                return ball;
            }
        }
        return null;
    }

    public void ResetBalls()
    {
        foreach (GameObject ball in activeBalls)
        {
            ball.SetActive(false);
        }

        activeBalls.Clear();

        foreach (GameObject ball in allBalls)
        {
            ball.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            GetBall();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetBalls();
        }
    }
}
