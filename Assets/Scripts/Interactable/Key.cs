using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    private const string KEY_NAME = "Key";

    [SerializeField] private Sprite _keySprite;

    public void InteractBegin(Player playerInteraction)
    {
        PlayerInventory playerInventory = playerInteraction.GetComponent<PlayerInventory>();
        playerInventory.AddInventoryItem(KEY_NAME, _keySprite);

        AudioManager.Instance.PlayPickUpKeySound();

        Destroy(gameObject);
    }

    public void InteractEnd()
    {

    }
}
