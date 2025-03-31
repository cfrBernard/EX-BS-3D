using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Transform[] shotPositions;
    public BallThrowSettings[] throwSettings;

    private int currentPositionIndex = 0;
    private BallGrabber ballGrabber;

    void Start()
    {
        ballGrabber = GetComponent<BallGrabber>();
        if (shotPositions.Length > 0) 
            SwitchPosition(0); // Set default position
    }

    void Update()
    {
        HandlePositionSwitch();
    }

    void HandlePositionSwitch()
    {
        for (int i = 0; i < shotPositions.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i)) 
            {
                SwitchPosition(i);
            }
        }
    }

    void SwitchPosition(int index)
    {
        if (index >= 0 && index < shotPositions.Length && index < throwSettings.Length)
        {
            currentPositionIndex = index;

            // Move the player
            transform.position = shotPositions[index].position;
            transform.rotation = shotPositions[index].rotation;

            // Assign the correct scriptable to the BallGrabber
            ballGrabber.throwSettings = throwSettings[index];

            Debug.Log($"Position changed: {shotPositions[index].name}, Type: {throwSettings[index].name}");
        }
    }
}
