using UnityEngine;

public class Coin : MonoBehaviour, IInteractable
{
    [SerializeField] private int coinAmount = 10;

    public void InteractBegin(Player player)
    {
        PlayerCoins.Instance.AddCoins(coinAmount);

        CoinsUI.Instance.UpdateCoinsUI();

        AudioManager.Instance.PlayPickUpCoinSound();

        Destroy(gameObject);
    }

    public void InteractEnd()
    {

    }
}
