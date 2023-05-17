using UnityEngine;

[CreateAssetMenu()]
public class AudioClipsSO : ScriptableObject
{
    [Header("Player")]
    public AudioClip playerJumpSound;
    public AudioClip playerLandSound;
    public AudioClip playerWalkSound;
    public AudioClip playerHurtSound;
    [Header("Interactable")]
    public AudioClip pickUpKeySound;
    public AudioClip pickUpCoinSound;
    public AudioClip leverOnSound;
    public AudioClip leverOffSound;
    public AudioClip jumpPadSound;
    public AudioClip disappearingPlatformSound_Disappear;
    public AudioClip disappearingPlatformSound_Activate;
    [Header("Enemies")]
    public AudioClip enemyExplosionSound;
    [Header("Game")]
    public AudioClip nextLevel;
    [Header("UI")]
    public AudioClip selectSound;
    [Header("Music")]
    public AudioClip menuMusic;
    public AudioClip gameMusic;
}
