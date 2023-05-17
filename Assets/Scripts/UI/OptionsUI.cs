using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsUI : MonoBehaviour
{
    private const string RESOLUTION_PLAYER_PREFS = "ScreenResolutionPlayerPrefs";
    private const string SCREEN_MODE_PLAYER_PREFS = "ScreenModePlayerPrefs";
    private const string MASTER_VOLUME_PLAYER_PREFS = "MasterVolumePlayerPrefs";

    private const float ASPECT_RATIO_16_X_9 = 1.77f;

    public static OptionsUI Instance { get; private set; }

    [Header("Graphics")]
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private TextMeshProUGUI _resolutionDropdownText;
    [SerializeField] private Toggle _screenModeToggle;

    [Header("Audio")]
    [SerializeField] private Slider _masterVolumeSlider;

    [Space]
    [SerializeField] private Button _resetButton;
    [SerializeField] private Button _backButton;
    [Space]
    [SerializeField] private ResetWindowUI _resetWindowUI;

    private Action OnBackButtonAction;

    //Resolution
    private Resolution[] _resolutionArray;
    private readonly List<Resolution> _filteredResolutionsList = new();
    private int _currentResolutionIndex;
    private int _currentRefreshRate;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one OptionsUI! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        InitializeFilteredResolutionsDropdown();

        _resolutionDropdown.onValueChanged.AddListener((int resolutionIndex) =>
        {
            SetResolution(resolutionIndex);
            PlayerPrefs.SetInt(RESOLUTION_PLAYER_PREFS, resolutionIndex);
        });

        _screenModeToggle.onValueChanged.AddListener((bool isFullScreen) =>
        {
            Screen.fullScreen = isFullScreen;
            PlayerPrefs.SetInt(SCREEN_MODE_PLAYER_PREFS, isFullScreen ? 1 : 0);
        });

        _masterVolumeSlider.onValueChanged.AddListener((float value) =>
        {
            AudioListener.volume = value;
            PlayerPrefs.SetFloat(MASTER_VOLUME_PLAYER_PREFS, value);
        });

        _resetButton.onClick.AddListener(() =>
        {
            Hide();
            _resetWindowUI.Show();
        });

        _backButton.onClick.AddListener(() =>
        {
            Hide();
            OnBackButtonAction?.Invoke();
        });

        UpdateSettingsValues();

        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
        _resolutionDropdown.Select();

        UpdateSettingsValues();
    }

    public void Show(Action onBackButtonAction)
    {
        gameObject.SetActive(true);
        _resolutionDropdown.Select();

        UpdateSettingsValues();

        OnBackButtonAction = onBackButtonAction;
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void UpdateSettingsValues()
    {
        _resolutionDropdown.value = PlayerPrefs.GetInt(RESOLUTION_PLAYER_PREFS);
        _screenModeToggle.isOn = PlayerPrefs.GetInt(SCREEN_MODE_PLAYER_PREFS) == 1;

        _masterVolumeSlider.value = PlayerPrefs.GetFloat(MASTER_VOLUME_PLAYER_PREFS);
        AudioListener.volume = PlayerPrefs.GetFloat(MASTER_VOLUME_PLAYER_PREFS);
    }

    #region Resolution

    private void InitializeFilteredResolutionsDropdown()
    {
        _resolutionArray = Screen.resolutions;

        _resolutionDropdown.ClearOptions();
        _currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = _resolutionArray.Length - 1; i > 0; i--)
        {
            if (_resolutionArray[i].refreshRate == _currentRefreshRate)
            {
                float aspectRatio = ((float)_resolutionArray[i].width / (float)_resolutionArray[i].height);
                aspectRatio = (float)Math.Floor(aspectRatio * 100) / 100;

                if (ASPECT_RATIO_16_X_9 == aspectRatio)
                {
                    _filteredResolutionsList.Add(_resolutionArray[i]);
                }
            }
        }

        List<string> dropdownResolutionOptionsTextList = new();

        for (int i = 0; i < _filteredResolutionsList.Count; i++)
        {
            string resolutionOptionText = _filteredResolutionsList[i].width + "x" + _filteredResolutionsList[i].height;
            dropdownResolutionOptionsTextList.Add(resolutionOptionText);
            if (_filteredResolutionsList[i].width == Screen.width && _filteredResolutionsList[i].height == Screen.height)
            {
                _currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(dropdownResolutionOptionsTextList);
        _resolutionDropdown.value = _currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    private void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _filteredResolutionsList[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen, _currentRefreshRate);
    }

    #endregion
}
