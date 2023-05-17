using System.Collections;
using UnityEngine;

public class Flag : MonoBehaviour, IInteractable
{
    [SerializeField] private SceneLoader.Scene _nextScene;
    [SerializeField] private GameObject _tipSprite;
    [SerializeField] private float _timeBeforeNextScene = 1f;

    private bool _playerInRange;

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

    private void Instance_OnInteractPerformed(object sender, System.EventArgs e)
    {
        if (!_playerInRange) 
        { 
            return; 
        }

        AudioManager.Instance.PlayOnLevelEndSound();

        SceneTransitionUI.Instance.FadeIn();

        StartCoroutine(NextScene());
    }

    private IEnumerator NextScene()
    {
        yield return new WaitForSeconds(_timeBeforeNextScene);

        SceneLoader.Load(_nextScene);

        if (_nextScene != SceneLoader.Scene.WinScene)
        {
            GameManager.Instance.SaveUnlockedLevelIndex((int)_nextScene);
        }

        PlayerCoins.Instance.SaveCoinsAmount();
    }

    private void SetTipSpriteVisible(bool isVisible)
    {
        _tipSprite.SetActive(isVisible);
    }
}
