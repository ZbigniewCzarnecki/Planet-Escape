using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    public event EventHandler OnGameOver;

    [SerializeField] private float _timeBeforeGameOver = 2f;

    private bool _isGamePaused = false;

    private int _unlockedLevelIndex;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("There is more than one GameManager: " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        InputManager.Instance.OnPausePerformed += InputManager_OnPausePerformed;
        PlayerHealth.Instance.OnDeath += PlayerHealth_OnDeath;

        _unlockedLevelIndex = SaveData.Instance.UnlockedLevelIndex;
    }

    private void InputManager_OnPausePerformed(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    public void TogglePauseGame()
    {
        _isGamePaused = !_isGamePaused;

        if (_isGamePaused)
        {
            Time.timeScale = 0;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public void ResetTimeScale()
    {
        Time.timeScale = 1;
    }

    private void PlayerHealth_OnDeath(object sender, System.EventArgs e)
    {
        StartCoroutine(GameOver());
    }

    public IEnumerator GameOver()
    {
        SceneTransitionUI.Instance.FadeIn();

        OnGameOver?.Invoke(this, EventArgs.Empty);

        yield return new WaitForSeconds(_timeBeforeGameOver);
        SceneLoader.Load(SceneLoader.GetCurrentTargetScene());
    }

    public bool IsGamePaused()
    {
        return _isGamePaused;
    }

    public void SaveUnlockedLevelIndex(int unlockedLevelIndex)
    {
        if (unlockedLevelIndex <= _unlockedLevelIndex)
        {
            return;
        }

        _unlockedLevelIndex = unlockedLevelIndex;

        SaveData.Instance.UnlockedLevelIndex = unlockedLevelIndex;
        SaveData.Instance.Save();
    }

    public int GetUnlockedLevelIndex()
    {
        return _unlockedLevelIndex;
    }
}
