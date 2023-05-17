using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _replayButton;
    [SerializeField] private Button _menuButton;

    private void Awake()
    {
        _resumeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.TogglePauseGame();
        });

        _replayButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ResetTimeScale();
            SceneLoader.Load(SceneLoader.GetCurrentTargetScene());
        });

        _menuButton.onClick.AddListener(() =>
        {
            GameManager.Instance.ResetTimeScale();
            SceneLoader.Load(SceneLoader.Scene.MainMenuScene);
        });
    }

    private void Start()
    {
        GameManager.Instance.OnGamePaused += GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnpaused += GameManager_OnGameUnpaused;

        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }

    private void GameManager_OnGameUnpaused(object sender, EventArgs e)
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _resumeButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
