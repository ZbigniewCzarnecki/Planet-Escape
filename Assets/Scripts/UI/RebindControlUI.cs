using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RebindControlUI : MonoBehaviour
{
    public static RebindControlUI Instance { get; private set; }

    [Header("Keyboard")]
    [SerializeField] private Button _keyboardMoveRightButton;
    [SerializeField] private Button _keyboardMoveLeftButton;
    [SerializeField] private Button _keyboardJumpButton;
    [SerializeField] private Button _keyboardInteractButton;
    [SerializeField] private TextMeshProUGUI _keyboardMoveRightButtonText;
    [SerializeField] private TextMeshProUGUI _keyboardMoveLeftButtonText;
    [SerializeField] private TextMeshProUGUI _keyboardJumpButtonText;
    [SerializeField] private TextMeshProUGUI _keyboardInteractButtonText;

    [Header("Gamepad")]
    [SerializeField] private Button _gamepadMoveRightButton;
    [SerializeField] private Button _gamepadMoveLeftButton;
    [SerializeField] private Button _gamepadJumpButton;
    [SerializeField] private Button _gamepadInteractButton;
    [SerializeField] private TextMeshProUGUI _gamepadMoveRightButtonText;
    [SerializeField] private TextMeshProUGUI _gamepadMoveLeftButtonText;
    [SerializeField] private TextMeshProUGUI _gamepadJumpButtonText;
    [SerializeField] private TextMeshProUGUI _gamepadInteractButtonText;

    [Header("RebindKeyUI")]
    [SerializeField] private Transform _rebindUI;

    [Space]
    [SerializeField] private Button _backButton;

    private Action OnBackButtonAction;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one RebindControlUI! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _keyboardMoveRightButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.MoveRight); });
        _keyboardMoveLeftButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.MoveLeft); });
        _keyboardJumpButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Jump); });
        _keyboardInteractButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Interact); });

        _gamepadMoveRightButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Gamepad_MoveRight); });
        _gamepadMoveLeftButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Gamepad_MoveLeft); });
        _gamepadJumpButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Gamepad_Jump); });
        _gamepadInteractButton.onClick.AddListener(() => { RebindBinding(InputManager.Binding.Gamepad_Interact); });

        _backButton.onClick.AddListener(() =>
        {
            Hide();
            OnBackButtonAction?.Invoke();
        });

        Hide();
    }

    private void Start()
    {
        UpdateRebindingButtonsText();
    }

    private void RebindBinding(InputManager.Binding binding)
    {
        ShowRebindUI();
        InputManager.Instance.RebindBinding(binding, () =>
        {
            HideRebindUI();
            UpdateRebindingButtonsText();
        });
    }

    private void UpdateRebindingButtonsText()
    {
        //Keyboard
        _keyboardMoveRightButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.MoveRight);
        _keyboardMoveLeftButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.MoveLeft);
        _keyboardJumpButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Jump);
        _keyboardInteractButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Interact);

        //Gamepad
        _gamepadMoveRightButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Gamepad_MoveRight);
        _gamepadMoveLeftButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Gamepad_MoveLeft);
        _gamepadJumpButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Gamepad_Jump);
        _gamepadInteractButtonText.text = InputManager.Instance.GetBindingText(InputManager.Binding.Gamepad_Interact);
    }

    public void ShowRebindUI()
    {
        _rebindUI.gameObject.SetActive(true);
    }

    public void HideRebindUI()
    {
        _rebindUI.gameObject.SetActive(false);
    }

    public void Show(Action onBackButtonAction)
    {
        gameObject.SetActive(true);

        _keyboardMoveRightButton.Select();

        OnBackButtonAction = onBackButtonAction;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
