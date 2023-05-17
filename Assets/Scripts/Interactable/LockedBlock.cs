using UnityEngine;
using UnityEngine.Events;

public class LockedBlock : MonoBehaviour, IInteractable
{

    [SerializeField] private string _keyName;
    [SerializeField] private GameObject _tipSprite;

    [SerializeField] private UnityEvent OnUnlockEvent;

    private bool _playerInRange;

    private PlayerInventory _playerInventory;

    private void Start()
    {
        InputManager.Instance.OnInteractPerformed += Instance_OnInteractPerformed;

        SetTipSpriteVisible(false);
    }

    public void InteractBegin(Player playerInteraction)
    {
        _playerInRange = true;

        if (playerInteraction.TryGetComponent<PlayerInventory>(out PlayerInventory playerInventory))
        {
            _playerInventory = playerInventory;
        }

        SetTipSpriteVisible(true);
    }

    public void InteractEnd()
    {
        _playerInRange = false;

        _playerInventory = null;

        SetTipSpriteVisible(false);
    }

    private void Instance_OnInteractPerformed(object sender, System.EventArgs e)
    {
        if (!_playerInRange || _playerInventory == null) 
        {
            return;
        }

        if (_playerInventory.GetInventoryItem(_keyName))
        {
            _playerInventory.RemoveInventoryItem(_keyName);

            OnUnlockEvent?.Invoke();
        }
    }

    private void SetTipSpriteVisible(bool isVisible)
    {
        _tipSprite.SetActive(isVisible);
    }
}
