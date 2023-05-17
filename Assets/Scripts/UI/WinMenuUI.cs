using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinMenuUI : MonoBehaviour
{
    [SerializeField] private Button _menuButton;

    [SerializeField] private TextMeshProUGUI _deathsText;
    [SerializeField] private TextMeshProUGUI _coinsText;

    private void Awake()
    {
        _menuButton.Select();

        _menuButton.onClick.AddListener(() =>
        {
            SceneLoader.Load(SceneLoader.Scene.MainMenuScene);
        });

        _deathsText.text = "Deaths: " + SaveData.Instance.DeathsAmount.ToString();
        _coinsText.text = "Coins: " + SaveData.Instance.CoinsAmount.ToString();
    }
}
