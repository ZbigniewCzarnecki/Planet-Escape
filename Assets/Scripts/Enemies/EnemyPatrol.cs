using System;

public class EnemyPatrol : Enemy
{
    public override event EventHandler<int> OnDirectionChange;

    private void FixedUpdate()
    {
        if (CheckIfHittingPlayer() != null)
        {
            CheckIfHittingPlayer().DecreaseHealth();
        }

        if (CheckIfHittingWall() || !CheckIfHittingGround())
        {
            if (!_isTurning)
            {
                _isTurning = true;
                _direction = _direction == 1 ? -1 : 1;

                OnDirectionChange?.Invoke(this, _direction);
            }
        }
        else if (CheckIfHittingGround())
        {
            _isTurning = false;
        }

        _rb2d.velocity = new(_direction * _moveSpeed, _rb2d.velocity.y);
    }
}
