using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioClipsSO _audioClipsSO;

    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private AudioSource _sfxLongSource;

    private bool _isGameMusicPlaying;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one AudioManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        SceneLoader.OnLoadNewScen += SceneLoader_OnLoadNewScen;
        PlayerAnimator.OnStepOnGroundEvent += PlayerAnimator_OnStepEvent;

        PlayMenuMusic();
    }

    public void PlayPlayerJumpSound() => _sfxSource.PlayOneShot(_audioClipsSO.playerJumpSound);
    public void PlayPlayerLandSound() => _sfxSource.PlayOneShot(_audioClipsSO.playerLandSound);
    public void PlayPlayerWalkSound() => _sfxSource.PlayOneShot(_audioClipsSO.playerWalkSound);
    public void PlayPlayerHurtSound() => _sfxSource.PlayOneShot(_audioClipsSO.playerHurtSound);

    public void PlayPickUpKeySound() => _sfxSource.PlayOneShot(_audioClipsSO.pickUpKeySound);
    public void PlayPickUpCoinSound() => _sfxSource.PlayOneShot(_audioClipsSO.pickUpCoinSound);
    public void PlayJumpPadSound() => _sfxSource.PlayOneShot(_audioClipsSO.jumpPadSound);
    public void PlayLeverOnSound() => _sfxSource.PlayOneShot(_audioClipsSO.leverOnSound);
    public void PlayLeverOffSound() => _sfxSource.PlayOneShot(_audioClipsSO.leverOffSound);
    public void PlayDisappearingPlatformSound_Disappear() => _sfxSource.PlayOneShot(_audioClipsSO.disappearingPlatformSound_Disappear);
    public void PlayDisappearingPlatformSound_Activate()
    {
        _sfxLongSource.clip = _audioClipsSO.disappearingPlatformSound_Activate;
        _sfxLongSource.Play();
    }

    public void PlayEnemyExplosion() => _sfxSource.PlayOneShot(_audioClipsSO.enemyExplosionSound);

    public void PlayOnLevelEndSound() => _sfxSource.PlayOneShot(_audioClipsSO.nextLevel);

    public void PlaySelectSound() => _sfxSource.PlayOneShot(_audioClipsSO.selectSound);

    public void PlayMenuMusic()
    {
        _musicSource.clip = _audioClipsSO.menuMusic;
        _musicSource.Play();
    }

    public void PlayGameMusic()
    {
        _musicSource.clip = _audioClipsSO.gameMusic;
        _musicSource.Play();
    }

    public void StopPlayingSFXLongSource()
    {
        _sfxLongSource.Stop();
    }

    private void PlayerAnimator_OnStepEvent(object sender, System.EventArgs e)
    {
        PlayPlayerWalkSound();
    }

    private void SceneLoader_OnLoadNewScen(object sender, System.EventArgs e)
    {
        if (SceneLoader.GetCurrentTargetScene() == SceneLoader.Scene.MainMenuScene)
        {
            PlayMenuMusic();
            _isGameMusicPlaying = false;
        }
        else
        {
            if (!_isGameMusicPlaying)
            {
                PlayGameMusic();
                _isGameMusicPlaying = true;
            }
        }
    }
}
