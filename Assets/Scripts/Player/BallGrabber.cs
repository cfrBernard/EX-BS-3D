using UnityEngine;

public class BallGrabber : MonoBehaviour
{
    public Camera playerCamera;
    public Transform throwPoint;
    public float grabSpeed = 10f;
    public float sensitivity = 0.5f;
    public float maxOffsetY = 0.5f;
    public float maxOffsetZ = 0.5f;
    public BallThrowSettings throwSettings;

    public AudioClip grabSound;
    public AudioSource audioSource;

    private GameObject grabbedBall = null;
    private Rigidbody grabbedRb;
    private bool isGrabbing = false;
    private Vector3 lastMousePosition;
    private Vector3 mouseVelocity;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) { TryGrabBall(); }
        if (Input.GetMouseButton(0) && grabbedBall != null) { HoldBall(); }
        if (Input.GetMouseButtonUp(0)) { ThrowBall(); }
    }

    void TryGrabBall()
    {
        RaycastHit hit;
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 20f))  // Grab distance
        {
            if (hit.collider.CompareTag("Ball"))
            {
                grabbedBall = hit.collider.gameObject;
                grabbedRb = grabbedBall.GetComponent<Rigidbody>();
                grabbedRb.useGravity = false;
                grabbedRb.freezeRotation = true;
                grabbedRb.linearVelocity = Vector3.zero;
                isGrabbing = true;
                lastMousePosition = Input.mousePosition;

                if (grabSound != null)
                {
                    audioSource.PlayOneShot(grabSound);
                }
            }
        }
    }

    void HoldBall()
    {
        if (grabbedBall == null) return;

        // Calculate vertical mouse position (between 0 and 1)
        float mouseYNormalized = Mathf.Clamp01(Input.mousePosition.y / Screen.height);
        float yOffset = Mathf.Lerp(0, maxOffsetY, mouseYNormalized);
        float zOffset = Mathf.Lerp(0, maxOffsetZ, mouseYNormalized);

        // Local offset
        Vector3 localOffset = new Vector3(0, yOffset, zOffset);
        Vector3 worldOffset = throwPoint.TransformDirection(localOffset);
        Vector3 targetPosition = throwPoint.position + worldOffset;

        // Gradual movement
        grabbedBall.transform.position = Vector3.Lerp(grabbedBall.transform.position, targetPosition, Time.deltaTime * grabSpeed);

        // Calculate mouse movement speed
        Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
        mouseVelocity = deltaMouse * sensitivity;
        lastMousePosition = Input.mousePosition;
    }

    void ThrowBall()
    {
        if (grabbedBall == null) return;

        grabbedRb.useGravity = true;
        grabbedRb.freezeRotation = false;

        // Throw direction
        Vector3 throwDirection = playerCamera.transform.forward;
        throwDirection += Vector3.up * throwSettings.angleUpward;
        throwDirection += playerCamera.transform.right * (mouseVelocity.x * throwSettings.horizontalInfluence);
        throwDirection += playerCamera.transform.up * (mouseVelocity.y * throwSettings.verticalInfluence);
        throwDirection.Normalize();

        // Calculate throw strength
        float throwStrength = Mathf.Clamp(mouseVelocity.magnitude * throwSettings.powerMultiplier,
                                          throwSettings.minThrowPower, 
                                          throwSettings.maxThrowPower);

        // Backspin
        grabbedRb.AddTorque(playerCamera.transform.right * throwSettings.spin);

        grabbedRb.linearVelocity = throwDirection * throwStrength;
        ScoreManager.Instance.IncrementThrowCount();

        grabbedBall = null;
        isGrabbing = false;
    }

}
