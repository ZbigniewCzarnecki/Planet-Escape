using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour, IInteractable
{
    private const string IS_ACTIVE = "IsActive";

    [SerializeField] private UnityEvent<bool> _leverActivateEvent;

    [SerializeField] private GameObject _leverTipSprite;
    private Animator _animator;

    private bool _playerInRange;
    private bool _isActive;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        InputManager.Instance.OnInteractPerformed += Instance_OnInteractPerformed;

        SetTipSpriteVisible(false);
    }

    public void InteractBegin(Player player)
    {
        _playerInRange = true;

        SetTipSpriteVisible(true);
    }

    public void InteractEnd()
    {
        _playerInRange = false;

        SetTipSpriteVisible(false);
    }

    //Function is performed in the ANIMATION for better synchronization of the animation with the event.
    public void InvokeLeverActivateEvent()
    {
        _leverActivateEvent?.Invoke(_isActive);
    }

    private void Instance_OnInteractPerformed(object sender, System.EventArgs e)
    {
        if (!_playerInRange) return;

        _isActive = !_isActive;

        _animator.SetBool(IS_ACTIVE, _isActive);

        if (_isActive)
        {
            AudioManager.Instance.PlayLeverOnSound();
        }
        else
        {
            AudioManager.Instance.PlayLeverOffSound();
        }
    }

    private void SetTipSpriteVisible(bool isVisible)
    {
        _leverTipSprite.SetActive(isVisible);
    }
}