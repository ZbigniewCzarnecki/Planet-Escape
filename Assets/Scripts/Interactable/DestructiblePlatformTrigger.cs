using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class DestructiblePlatformTrigger : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent OnDestroyEvent;

    private const string ACTIVATE = "Activate";
    private const string DESTROY = "Destroy";
    private const string FADE_IN = "FadeIn";

    [SerializeField] private Animator _animator;
    [SerializeField] private BoxCollider2D _triggerCollider;

    [SerializeField] private float _timeToDestroy = 1f;
    [SerializeField] private float _timeToFadeIn = 2f;

    public void InteractBegin(Player player)
    {
        _triggerCollider.enabled = false;

        AudioManager.Instance.PlayDisappearingPlatformSound_Activate();

        StartCoroutine(DestroyPlatform());
        SetTrigger(ACTIVATE);
    }

    public void InteractEnd()
    {

    }

    private IEnumerator DestroyPlatform()
    {
        yield return new WaitForSeconds(_timeToDestroy);

        StartCoroutine(FadeInPlatform());
        SetTrigger(DESTROY);

        AudioManager.Instance.StopPlayingSFXLongSource();
        AudioManager.Instance.PlayDisappearingPlatformSound_Disappear();

        OnDestroyEvent?.Invoke();
    }

    private IEnumerator FadeInPlatform()
    {
        yield return new WaitForSeconds(_timeToFadeIn);
        _triggerCollider.enabled = true;
        SetTrigger(FADE_IN);
    }

    private void SetTrigger(string triggerName)
    {
        _animator.SetTrigger(triggerName);
    }
}
