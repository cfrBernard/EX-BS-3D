using UnityEngine;

public class BallSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip groundSound;
    public AudioClip fenceSound;
    public AudioClip backboardSound; 
    public AudioClip poleSound;
    public AudioClip ballSound;

    public float minImpactSpeed = 1f;
    public float groundCooldownTime = 0.5f;
    private float lastGroundCollisionTime = 0f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (rb.linearVelocity.magnitude < minImpactSpeed) return;

        if (collision.collider.CompareTag("Ground"))
        {
            if (Time.time - lastGroundCollisionTime < groundCooldownTime) return;
            PlayGroundSound(groundSound);
            lastGroundCollisionTime = Time.time;
        }
        else if (collision.collider.CompareTag("Fence"))
        {
            PlaySound(fenceSound);
        }
        else if (collision.collider.CompareTag("Backboard"))
        {
            PlaySound(backboardSound);
        }
        else if (collision.collider.CompareTag("Pole"))
        {
            PlaySound(poleSound);
        }
        else if (collision.collider.CompareTag("Ball"))
        {
            PlaySound(ballSound);
        }
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            // Adjusts volume and pitch based on impact speed
            audioSource.volume = Mathf.Clamp(rb.linearVelocity.magnitude / 10f, 0.1f, 1f);
            audioSource.pitch = Mathf.Clamp(1 + rb.linearVelocity.magnitude / 10f, 1f, 2f);
            audioSource.PlayOneShot(clip);
        }
    }

    void PlayGroundSound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.volume = Mathf.Clamp(rb.linearVelocity.magnitude / 10f, 0.1f, 0.8f);
            audioSource.pitch = Mathf.Clamp(1 + rb.linearVelocity.magnitude / 10f, 1f, 1.1f);
            audioSource.PlayOneShot(clip);
        }
    }
}
