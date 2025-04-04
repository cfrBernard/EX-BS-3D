using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MenuManager : MonoBehaviour
{
    public Camera menuCamera;
    public Transform playerCameraTransform;
    public GameObject startText;
    public PauseManager pauseManager;
    public Image fadeImage;
    public AudioMixer mainMixer;
    public float transitionDuration = 1.5f;
    public float fadeDuration = 10f;

    private bool transitioning = false;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    void Update()
    {
        if (!transitioning && Input.anyKeyDown)
        {
            StartCoroutine(TransitionToPlayer());
        }
    }

    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        mainMixer.SetFloat("MasterVolume", -80f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float alpha = 1 - (elapsedTime / fadeDuration);
            fadeImage.color = new Color(0, 0, 0, alpha);

            float volume = Mathf.Lerp(-80f, 20f, elapsedTime / fadeDuration);
            mainMixer.SetFloat("MasterVolume", volume);

            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }

    IEnumerator TransitionToPlayer()
    {
        transitioning = true;
        startText.SetActive(false); // Hide start text

        float elapsedTime = 0f;
        Vector3 startPosition = menuCamera.transform.position;
        Quaternion startRotation = menuCamera.transform.rotation;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.unscaledDeltaTime; // smooth transition
            float t = Mathf.SmoothStep(0f, 1f, elapsedTime / transitionDuration);
            menuCamera.transform.position = Vector3.Lerp(startPosition, playerCameraTransform.position, t);
            menuCamera.transform.rotation = Quaternion.Slerp(startRotation, playerCameraTransform.rotation, t);
            yield return null;
        }

        menuCamera.gameObject.SetActive(false);
        
        // Show help menu
        pauseManager.TogglePause(); 
        pauseManager.OpenHelpMenu();
    }
}
