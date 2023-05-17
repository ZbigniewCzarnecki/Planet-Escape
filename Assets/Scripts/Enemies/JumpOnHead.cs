using UnityEngine;
using UnityEngine.Events;

public class JumpOnHead : MonoBehaviour
{
    [SerializeField] private LayerMask _playerLayerMask;
    [SerializeField] private float _playerLaunchForce = 30f;
    [SerializeField] private float _playerGravityScale = 8f;

    [SerializeField] private UnityEvent OnDestroyEvent;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_playerLayerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            if (collision.relativeVelocity.y < 0f)
            {
                Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

                LaunchPlayer(playerRb);

                AudioManager.Instance.PlayEnemyExplosion();

                OnDestroyEvent?.Invoke();

                Destroy(gameObject);
            }
        }
    }

    private void LaunchPlayer(Rigidbody2D playerRb)
    {
        playerRb.velocity = Vector2.zero;
        playerRb.gravityScale = _playerGravityScale;
        playerRb.AddForce(Vector2.up * _playerLaunchForce, ForceMode2D.Impulse);
    }
}
