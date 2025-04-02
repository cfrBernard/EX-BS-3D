using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HoopTrigger : MonoBehaviour
{
    public int scoreValue = 1;
    private HashSet<GameObject> registeredBalls = new HashSet<GameObject>();

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball") && !registeredBalls.Contains(other.gameObject))
        {
            Debug.Log("🏀 SCORE ! + " + scoreValue + " point(s)");
            ScoreManager.Instance.AddScore(scoreValue);
            registeredBalls.Add(other.gameObject);
            StartCoroutine(RemoveFromSetAfterDelay(other.gameObject, 3f)); 
        }
    }

    private IEnumerator RemoveFromSetAfterDelay(GameObject ball, float delay)
    {
        yield return new WaitForSeconds(delay);
        registeredBalls.Remove(ball);
    }
}
