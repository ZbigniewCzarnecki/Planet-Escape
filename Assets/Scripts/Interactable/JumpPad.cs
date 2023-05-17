using UnityEngine;

public class JumpPad : MonoBehaviour, IInteractable
{
    private const string ACTIVATE = "Activate";

    [SerializeField] private float _launchForce = 30f;
    [SerializeField] private float _gravityScale = 8f;
    [SerializeField] private Vector2 _direction = Vector2.up;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void InteractBegin(Player player)
    {
        if (player.gameObject.TryGetComponent(out Rigidbody2D rb2d))
        {
            _animator.SetTrigger(ACTIVATE);

            AudioManager.Instance.PlayJumpPadSound();

            rb2d.velocity = Vector2.zero;
            rb2d.gravityScale = _gravityScale;
            rb2d.AddForce(_direction * _launchForce, ForceMode2D.Impulse);
        }
    }

    public void InteractEnd()
    {

    }
}
