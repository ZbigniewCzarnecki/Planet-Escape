using UnityEngine;

public class EnemyBlock : Enemy
{
    [Header("Block")]
    [SerializeField] private float _waitTimeMax = 1f;
    private float _waitTime;

    private enum State
    {
        Wait,
        Rush
    }

    private State _state;

    private void FixedUpdate()
    {
        switch (_state)
        {
            case State.Wait:
                _rb2d.velocity = Vector2.zero;

                _waitTime += Time.deltaTime;

                if (_waitTime >= _waitTimeMax)
                {
                    _waitTime = 0;
                    _state = State.Rush;
                }
                break;
            case State.Rush:
                if (CheckIfHittingPlayer() != null)
                {
                    CheckIfHittingPlayer().DecreaseHealth();
                }

                if (CheckIfHittingWall() || !CheckIfHittingGround())
                {
                    _state = State.Wait;

                    if (!_isTurning)
                    {
                        _isTurning = true;
                        _direction = _direction == 1 ? -1 : 1;
                    }
                }
                else if (CheckIfHittingGround())
                {
                    _isTurning = false;
                }

                _rb2d.velocity = new(_direction * _moveSpeed, _rb2d.velocity.y);
                break;
        }
    }
}
