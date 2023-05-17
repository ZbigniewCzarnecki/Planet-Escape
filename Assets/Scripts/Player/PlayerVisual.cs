using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _playerMovement.OnDirectionChange += PlayerMovement_OnDirectionChange;

        _spriteRenderer.flipX = true;
    }

    private void PlayerMovement_OnDirectionChange(object sender, int direction)
    {
        _spriteRenderer.flipX = direction == 1;
    }
}
