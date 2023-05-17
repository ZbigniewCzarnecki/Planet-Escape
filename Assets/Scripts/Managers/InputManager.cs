using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private const string REBIND_KEYS_PLAYER_PREFS = "RebindKeysPlayerPrefs";

    public static InputManager Instance { get; private set; }

    public event EventHandler OnPausePerformed;
    public event EventHandler OnInteractPerformed;

    public event EventHandler OnKeyRebind;

    public enum Binding
    {
        MoveRight,
        MoveLeft,
        Jump,
        Interact,
        Gamepad_MoveRight,
        Gamepad_MoveLeft,
        Gamepad_Jump,
        Gamepad_Interact,
    }

    private PlayerInputActions _playerInputActions;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one InputManager " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _playerInputActions = new PlayerInputActions();

        //Load Binding Overrides after creating new PlayerInputActions and before Enableing it!
        if (PlayerPrefs.HasKey(REBIND_KEYS_PLAYER_PREFS))
        {
            _playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(REBIND_KEYS_PLAYER_PREFS));
        }

        _playerInputActions.Player.Enable();

        _playerInputActions.Player.Pause.performed += Pause_Performed;
        _playerInputActions.Player.Interact.performed += Interact_Performed;
    }

    private void OnDestroy()
    {
        _playerInputActions.Player.Pause.performed -= Pause_Performed;
        _playerInputActions.Player.Interact.performed -= Interact_Performed;

        _playerInputActions.Dispose();
    }

    private void Pause_Performed(InputAction.CallbackContext obj)
    {
        OnPausePerformed?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_Performed(InputAction.CallbackContext obj)
    {
        OnInteractPerformed?.Invoke(this, EventArgs.Empty);
    }

    public float GetHorizontalAxis()
    {
        if (_playerInputActions.Player.MoveRight.IsPressed()) return 1f;
        else if (_playerInputActions.Player.MoveLeft.IsPressed()) return -1f;
        else return 0f;
    }

    public bool GetJumpPressed()
    {
        return _playerInputActions.Player.Jump.WasPressedThisFrame();
    }

    public bool GetJumpRealeased()
    {
        return _playerInputActions.Player.Jump.WasReleasedThisFrame();
    }

    #region Rebind

    public string GetBindingText(Binding binding)
    {
        return binding switch
        {
            Binding.MoveRight => _playerInputActions.Player.MoveRight.bindings[0].ToDisplayString(),
            Binding.MoveLeft => _playerInputActions.Player.MoveLeft.bindings[0].ToDisplayString(),
            Binding.Jump => _playerInputActions.Player.Jump.bindings[0].ToDisplayString(),
            Binding.Interact => _playerInputActions.Player.Interact.bindings[0].ToDisplayString(),
            Binding.Gamepad_MoveRight => _playerInputActions.Player.MoveRight.bindings[1].ToDisplayString(),
            Binding.Gamepad_MoveLeft => _playerInputActions.Player.MoveLeft.bindings[1].ToDisplayString(),
            Binding.Gamepad_Jump => _playerInputActions.Player.Jump.bindings[1].ToDisplayString(),
            Binding.Gamepad_Interact => _playerInputActions.Player.Interact.bindings[1].ToDisplayString(),
            _ => "",
        };
    }

    public void RebindBinding(Binding binding, Action onActionRebound)
    {
        _playerInputActions.Player.Disable();

        InputAction inputAction;
        int bindingIndex;

        switch (binding)
        {
            default:
            case Binding.MoveRight:
                inputAction = _playerInputActions.Player.MoveRight;
                bindingIndex = 0;
                break;
            case Binding.MoveLeft:
                inputAction = _playerInputActions.Player.MoveLeft;
                bindingIndex = 0;
                break;
            case Binding.Jump:
                inputAction = _playerInputActions.Player.Jump;
                bindingIndex = 0;
                break;
            case Binding.Interact:
                inputAction = _playerInputActions.Player.Interact;
                bindingIndex = 0;
                break;
            case Binding.Gamepad_MoveRight:
                inputAction = _playerInputActions.Player.MoveRight;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_MoveLeft:
                inputAction = _playerInputActions.Player.MoveLeft;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Jump:
                inputAction = _playerInputActions.Player.Jump;
                bindingIndex = 1;
                break;
            case Binding.Gamepad_Interact:
                inputAction = _playerInputActions.Player.Interact;
                bindingIndex = 1;
                break;
        }

        inputAction.PerformInteractiveRebinding(bindingIndex)
            .OnComplete(callback =>
            {
                callback.Dispose();
                _playerInputActions.Player.Enable();
                onActionRebound();

                PlayerPrefs.SetString(REBIND_KEYS_PLAYER_PREFS, _playerInputActions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();

                OnKeyRebind?.Invoke(this, EventArgs.Empty);
            })
            .Start();
    }

    #endregion
}
