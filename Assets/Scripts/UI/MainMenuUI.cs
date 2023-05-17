using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _optionsButton;
    [SerializeField] private Button _controlButton;
    [SerializeField] private Button _quitButton;

    private void Awake()
    {
        _playButton.onClick.AddListener(() =>
        {
            Hide();
            UnlockedLevelsUI.Instance.Show(Show);
        });

        _optionsButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show(Show);
        });

        _controlButton.onClick.AddListener(() =>
        {
            Hide();
            RebindControlUI.Instance.Show(Show);
        });

        _quitButton.onClick.AddListener(() =>
        {
            QuitGame();
        });

        _playButton.Select();
    }

    private void Show()
    {
        gameObject.SetActive(true);
        _playButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private static void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
