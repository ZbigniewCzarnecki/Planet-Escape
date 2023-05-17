using TMPro;
using UnityEngine;

public class CoinsUI : MonoBehaviour
{
    public static CoinsUI Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI _coinsText;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one CoinsUI! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        UpdateCoinsUI();
    }

    public void UpdateCoinsUI()
    {
        _coinsText.text = PlayerCoins.Instance.GetCurrentCoinsAmount().ToString();
    }
}
