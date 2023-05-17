using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private LayerMask _platformLayerMask;

    public event EventHandler<int> OnDirectionChange;

    [SerializeField] private float _moveSpeedMax = 6f;
    [SerializeField] private float _acceleration = 20f;
    [SerializeField] private float _deacceleration = 20f;
    private float _currentSpeed;
    private float _horizontalInput;
    private int _newDirection;
    private int _direction;

    [SerializeField] private float _jumpHeight = 2.5f;
    [SerializeField] private float _gravityScale = 3f;
    [SerializeField] private float _fallingGravityScale = 8f;

    private float _timeToJump;
    [SerializeField] private float _timeToJumpMax = 0.15f;

    private bool _isMoving;
    private bool _isJumping;
    private bool _preventMoving;

    private Rigidbody2D _rb2d;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleJump();
        HandleSpriteDirection();
    }

    private void HandleSpriteDirection()
    {
        if (_newDirection != _direction)
        {
            _direction = _newDirection;

            OnDirectionChange?.Invoke(this, _direction);
        }
    }

    private void HandleJump()
    {
        if (IsGrounded() && InputManager.Instance.GetJumpPressed())
        {
            Jump();
        }

        if (_isJumping)
        {
            if (_rb2d.velocity.y > 0 && InputManager.Instance.GetJumpRealeased())
            {
                _rb2d.gravityScale = _fallingGravityScale;
            }

            if (_rb2d.velocity.y <= 0)
            {
                _rb2d.gravityScale = _fallingGravityScale;

                if (IsGrounded())
                {
                    _isJumping = false;

                    AudioManager.Instance.PlayPlayerLandSound();
                }
            }
        }

        if (!_isJumping && !IsGrounded())
        {
            _timeToJump += Time.deltaTime;

            if (_timeToJump < _timeToJumpMax && InputManager.Instance.GetJumpPressed())
            {
                Jump();
            }
        }

        if (IsGrounded())
        {
            _timeToJump = 0;
        }
    }

    private void Jump()
    {
        _rb2d.velocity = Vector2.zero;
        _rb2d.gravityScale = _gravityScale;
        float jumpForce = Mathf.Sqrt(_jumpHeight * (Physics2D.gravity.y * _rb2d.gravityScale) * -2) * _rb2d.mass;
        _rb2d.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _isJumping = true;

        AudioManager.Instance.PlayPlayerJumpSound();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        _horizontalInput = InputManager.Instance.GetHorizontalAxis();
        _isMoving = Mathf.Abs(_horizontalInput) != 0;

        if (_isMoving)
        {
            _currentSpeed += _acceleration * Time.fixedDeltaTime;
            _newDirection = Mathf.RoundToInt(_horizontalInput);
        }
        else
        {
            _currentSpeed -= _deacceleration * Time.fixedDeltaTime;
        }

        _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _moveSpeedMax);

        if (!_preventMoving)
        {
            _rb2d.velocity = new Vector2(_newDirection * _currentSpeed, _rb2d.velocity.y);
        }
    }

    public bool IsGrounded()
    {
        float raycastDistance = .8f;

        Debug.DrawRay(transform.position, Vector2.down * raycastDistance, Color.red);

        return Physics2D.Raycast(transform.position, Vector2.down, raycastDistance, _platformLayerMask);
    }

    public bool IsMoving()
    {
        return _isMoving;
    }

    public bool IsJumping()
    {
        return _isJumping;
    }

    public bool IsFalling()
    {
        return _rb2d.velocity.y < 0;
    }

    public int GetMoveDirection()
    {
        return _newDirection;
    }

    public float GetCurrentSpeed()
    {
        return _currentSpeed;
    }

    public void PreventFromMoving(bool preventMoving)
    {
        _preventMoving = preventMoving;
    }
}