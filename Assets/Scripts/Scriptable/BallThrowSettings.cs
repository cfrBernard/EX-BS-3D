using UnityEngine;

[CreateAssetMenu(fileName = "NewThrowSettings", menuName = "Basketball/Throw Settings")]
public class BallThrowSettings : ScriptableObject
{
    [Header("🎯 Angle & Direction")] 
    [Tooltip("Upward inclination of the throw. Increase if the ball goes too low.")]
    [Range(0f, 0.5f)] 
    public float angleUpward = 0.1f;
    [Tooltip("Influence of horizontal mouse movement on the shot direction.")]
    [Range(0f, 0.1f)] 
    public float horizontalInfluence = 0.02f;
    [Tooltip("Influence of vertical mouse movement on the shot direction.")]
    [Range(0f, 0.1f)] 
    public float verticalInfluence = 0.04f;

    
    [Header("🚀 Throw Power")]
    [Tooltip("Minimum throw power, even if the mouse moves slowly.")]
    [Range(0f, 10f)] 
    public float minThrowPower = 3f;
    [Tooltip("Maximum throw power to prevent overly powerful shots.")]
    [Range(10f, 20f)] 
    public float maxThrowPower = 15f;
    [Tooltip("Multiplier for mouse speed to calculate shot power.")]
    [Range(0.01f, 0.5f)] 
    public float powerMultiplier = 0.1f;

    
    [Header("🎯 Feeling & Feedback")]
    [Tooltip("Adds a slight delay before the ball is released.")]
    [Range(0f, 0.2f)] 
    public float releaseDelay = 0f;
    [Tooltip("Adds a backspin effect.")]
    [Range(0f, 100f)] 
    public float spin = 50f;
}
