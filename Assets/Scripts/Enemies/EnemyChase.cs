using System;
using System.Collections;
using UnityEngine;

public class EnemyChase : Enemy
{
    public override event EventHandler<int> OnDirectionChange;

    [Header("Chase")]
    [SerializeField] private float _chaseSpeed = 4f;
    [SerializeField] private float _jumpForce = 15f;
    [SerializeField] private float _gravityScale = 8f;
    [SerializeField] private float _chaseDistance = 6f;

    private enum State
    {
        Patrol,
        Chase
    }

    private State _state;

    private bool _playerInRange;
    private bool _isJumping;

    private Transform _playerTransform;

    private void Start()
    {
        _state = State.Patrol;
    }

    private void FixedUpdate()
    {
        switch (_state)
        {
            case State.Patrol:
                if (_playerInRange) _state = State.Chase;

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
                break;
            case State.Chase:
                if (!_playerInRange)
                {
                    StopAllCoroutines();
                    _state = State.Patrol;
                }

                if (CheckIfHittingPlayer() != null)
                {
                    CheckIfHittingPlayer().DecreaseHealth();
                }

                if (!_isJumping && CheckIfHittingWall())
                {
                    _rb2d.gravityScale = _gravityScale;
                    _rb2d.velocity = Vector2.zero;
                    _rb2d.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);

                    _isJumping = true;
                }
                else if (_isJumping && CheckIfHittingGround())
                {
                    _isJumping = false;
                }

                _rb2d.velocity = new(_direction * _chaseSpeed, _rb2d.velocity.y);
                break;
            default:
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_playerLayerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            _playerInRange = true;

            _playerTransform = collision.transform;

            _direction = _playerTransform.position.x < transform.position.x ? -1 : 1;
            OnDirectionChange?.Invoke(this, _direction);

            StartCoroutine(CheckPlayerPositionX());
        }
    }

    private IEnumerator CheckPlayerPositionX()
    {
        yield return new WaitForSeconds(0.5f);

        _direction = _playerTransform.position.x < transform.position.x ? -1 : 1;
        OnDirectionChange?.Invoke(this, _direction);

        if (Vector2.Distance(_playerTransform.position, transform.position) >= _chaseDistance)
        {
            _playerInRange = false;
        }

        StartCoroutine(CheckPlayerPositionX());
    }
}
