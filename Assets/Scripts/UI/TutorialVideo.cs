using UnityEngine;
using UnityEngine.Video;

public class TutorialVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer.Play();
    }
}
