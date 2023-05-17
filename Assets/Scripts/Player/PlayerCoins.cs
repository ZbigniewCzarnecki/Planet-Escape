using UnityEngine;

public class PlayerCoins : MonoBehaviour
{
    public static PlayerCoins Instance { get; private set; }

    private int _coins;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one PlayerCoins! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        LoadCoinsAmount();
    }

    public void AddCoins(int coinsAmount)
    {
        _coins += coinsAmount;
    }

    public int GetCurrentCoinsAmount()
    {
        return _coins;
    }

    private void LoadCoinsAmount()
    {
        _coins = SaveData.Instance.CoinsAmount;
    }

    public void SaveCoinsAmount()
    {
        SaveData.Instance.CoinsAmount = _coins;
        SaveData.Instance.Save();
    }
}
