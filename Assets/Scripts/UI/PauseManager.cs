using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject helpMenu;
    public Slider sensitivitySlider;
    public TextMeshProUGUI sensitivityValueText;
    
    private bool isPaused = false;
    private BallGrabber ballGrabber;

    void Start()
    {
        ballGrabber = FindAnyObjectByType<BallGrabber>();

        if (ballGrabber != null && ballGrabber.throwSettings != null)
        {
            sensitivitySlider.value = ballGrabber.throwSettings.powerMultiplier;
            UpdateSensitivityText(sensitivitySlider.value);
            sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (helpMenu.activeSelf)
            {
                CloseHelpMenu();
            }
            else
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0 : 1;
    }

    public void OpenHelpMenu()
    {
        pauseMenu.SetActive(false);
        helpMenu.SetActive(true);
    }

    public void CloseHelpMenu()
    {
        helpMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    void UpdateSensitivity(float newValue)
    {
        if (ballGrabber != null && ballGrabber.throwSettings != null)
        {
            ballGrabber.throwSettings.powerMultiplier = newValue;
            UpdateSensitivityText(newValue);
        }
    }

    void UpdateSensitivityText(float value)
    {
        sensitivityValueText.text = value.ToString("F2");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
