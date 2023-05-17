using UnityEngine;

public class EnemyVisual : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Enemy _enemy;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemy = GetComponentInParent<Enemy>();
    }

    private void Start()
    {
        _enemy.OnDirectionChange += EnemyBase_OnDirectionChange;
    }

    private void EnemyBase_OnDirectionChange(object sender, int direction)
    {
        _spriteRenderer.flipX = direction == 1;
    }
}
