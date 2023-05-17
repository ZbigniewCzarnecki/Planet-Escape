using UnityEngine;
using UnityEngine.UI;

public class UnlockedLevelButtonUI : MonoBehaviour
{
    private Button _button;
    [SerializeField] private SceneLoader.Scene _sceneToLoad;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void InitializeButton()
    {
        _button.onClick.AddListener(() =>
        {
            LoadScene();
        });
    }

    public void LoadScene()
    {
        SceneLoader.Load(_sceneToLoad);
    }

    public void SetButtonInteractable(bool interactable)
    {
        _button.interactable = interactable;
    }
}
