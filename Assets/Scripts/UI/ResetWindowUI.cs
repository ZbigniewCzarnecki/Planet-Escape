using UnityEngine;
using UnityEngine.UI;

public class ResetWindowUI : MonoBehaviour
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _resetButton;

    private void Awake()
    {
        _backButton.onClick.AddListener(() =>
        {
            Hide();
            OptionsUI.Instance.Show();
        });

        _resetButton.onClick.AddListener(() =>
        {
            SaveData.Instance.ResetData();

            Hide();
            OptionsUI.Instance.Show();
        });
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _backButton.Select();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
