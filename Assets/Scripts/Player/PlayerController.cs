using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public Transform[] shotPositions;
    public BallThrowSettings[] throwSettings;
    public float transitionSpeed = 5f;

    private int currentPositionIndex = 0;
    private BallGrabber ballGrabber;
    private bool isTransitioning = false;

    void Start()
    {
        ballGrabber = GetComponent<BallGrabber>();
        if (shotPositions.Length > 0) 
            SwitchPosition(0, true);
    }

    void Update()
    {
        HandlePositionSwitch();
    }

    void HandlePositionSwitch()
    {
        for (int i = 0; i < shotPositions.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i) && !isTransitioning)
            {
                StartCoroutine(SmoothTransition(i));
            }
        }
    }

    IEnumerator SmoothTransition(int index)
    {
        if (index >= 0 && index < shotPositions.Length && index < throwSettings.Length)
        {
            isTransitioning = true;

            Vector3 startPos = transform.position;
            Quaternion startRot = transform.rotation;
            Vector3 targetPos = shotPositions[index].position;
            Quaternion targetRot = shotPositions[index].rotation;

            float t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * transitionSpeed;
                transform.position = Vector3.Lerp(startPos, targetPos, t);
                transform.rotation = Quaternion.Slerp(startRot, targetRot, t);
                yield return null;
            }

            transform.position = targetPos;
            transform.rotation = targetRot;
            currentPositionIndex = index;
            ballGrabber.throwSettings = throwSettings[index];

            Debug.Log($"Position changed: {shotPositions[index].name}, Type: {throwSettings[index].name}");
            isTransitioning = false;
        }
    }

    void SwitchPosition(int index, bool instant)
    {
        if (index >= 0 && index < shotPositions.Length && index < throwSettings.Length)
        {
            currentPositionIndex = index;

            if (instant)
            {
                transform.position = shotPositions[index].position;
                transform.rotation = shotPositions[index].rotation;
            }
            else
            {
                StartCoroutine(SmoothTransition(index));
            }

            ballGrabber.throwSettings = throwSettings[index];
            Debug.Log($"Position changed: {shotPositions[index].name}, Type: {throwSettings[index].name}");
        }
    }
}
