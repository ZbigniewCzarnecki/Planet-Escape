using UnityEngine;

public class MovingPlatformTrigger : MonoBehaviour, IInteractable
{
    private bool _playerOnPlatform;

    private PlayerMovement _playerMovement;
    private Rigidbody2D _playerRigidbody;

    private float _platformVelocityX;
    private float _platformVelocityY;

    public void InteractBegin(Player player)
    {
        _playerMovement = player.GetComponent<PlayerMovement>();
        _playerRigidbody = player.GetComponent<Rigidbody2D>();

        _playerMovement.PreventFromMoving(true);

        _playerOnPlatform = true;
    }

    public void InteractEnd()
    {
        _playerMovement.PreventFromMoving(false);

        _playerOnPlatform = false;

        _playerMovement = null;
        _playerRigidbody = null;
    }

    void FixedUpdate()
    {
        //CarryingPlayer
        if (_playerOnPlatform)
        {
            if (!_playerMovement.IsMoving() && !_playerMovement.IsJumping())
            {
                _playerMovement.PreventFromMoving(true);
                _playerRigidbody.velocity = new(_platformVelocityX, _platformVelocityY);
            }
            else if (!_playerMovement.IsMoving() && _playerMovement.IsJumping())
            {
                _playerRigidbody.velocity = new(_platformVelocityX, _playerRigidbody.velocity.y);
            }
            else if (_playerMovement.IsMoving())
            {
                float playerDirection = _playerMovement.GetMoveDirection();

                if (_platformVelocityX < -0.1f && playerDirection < -0.1f || _platformVelocityX > 0.1f && playerDirection > 0.1f)
                {
                    _playerRigidbody.velocity = new(playerDirection * 8, _playerRigidbody.velocity.y);
                }
                else if ((_platformVelocityX > 0.1f && playerDirection < -0.1f || _platformVelocityX < -0.1f && playerDirection > 0.1f))
                {
                    _playerRigidbody.velocity = new(playerDirection * 4, _playerRigidbody.velocity.y);
                }
                else if (_platformVelocityX == 0f && Mathf.Abs(playerDirection) > 0.1f)
                {
                    _playerRigidbody.velocity = new(playerDirection * 6, _playerRigidbody.velocity.y);
                }
            }
        }
    }

    public void SetPlatformVelocityX(float platformVelocityX)
    {
        _platformVelocityX = platformVelocityX;
    }

    public void SetPlatformVelocityY(float platformVelocityY)
    {
        _platformVelocityY = platformVelocityY;
    }
}
