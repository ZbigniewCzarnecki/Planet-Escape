using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockedLevelsUI : MonoBehaviour
{
    public static UnlockedLevelsUI Instance { get; private set; }

    private readonly List<UnlockedLevelButtonUI> _unlockedLevelButtonsList = new();

    [SerializeField] private Button _selectButtonOnStart;
    [SerializeField] private Transform _buttonsContainer;

    [SerializeField] private Button _backButton;
    private Action OnBackButtonAction;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one UnlockedLevelsUI! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _backButton.onClick.AddListener(() =>
        {
            Hide();
            OnBackButtonAction?.Invoke();
        });
    }

    private void Start()
    {
        foreach (Transform child in _buttonsContainer)
        {
            UnlockedLevelButtonUI unlockedLevelButtonUI = child.GetComponent<UnlockedLevelButtonUI>();
            _unlockedLevelButtonsList.Add(unlockedLevelButtonUI);

            unlockedLevelButtonUI.InitializeButton();
        }

        Hide();

        UpdateInteractableButtons();
    }

    private void OnEnable()
    {
        UpdateInteractableButtons();
    }

    public void UpdateInteractableButtons()
    {
        for (int i = 0; i < _unlockedLevelButtonsList.Count; i++)
        {
            if (i < SaveData.Instance.UnlockedLevelIndex)
            {
                _unlockedLevelButtonsList[i].SetButtonInteractable(true);
            }
            else
            {
                _unlockedLevelButtonsList[i].SetButtonInteractable(false);
            }
        }
    }

    public void Show(Action backButtonAction)
    {
        gameObject.SetActive(true);

        _selectButtonOnStart.Select();

        OnBackButtonAction = backButtonAction;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
