using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public virtual event EventHandler<int> OnDirectionChange;

    protected Rigidbody2D _rb2d;

    [Header("Base")]
    [SerializeField] protected float _moveSpeed = 2f;
    [Header("Raycast")]
    [SerializeField] protected float _hitWallRaycastDistance = 0.6f;
    [SerializeField] protected float _hitGroundRaycastDistance = 1f;
    [SerializeField] protected float _hitGroundRaycastStartPosition = 0.45f;
    [Header("Layers")]
    [SerializeField] protected LayerMask _platformLayerMask;
    [SerializeField] protected LayerMask _playerLayerMask;

    protected int _direction = -1;
    protected bool _isTurning;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    protected bool CheckIfHittingWall()
    {
        RaycastHit2D raycastLeft = Physics2D.Raycast(transform.position, Vector2.left, _hitWallRaycastDistance, _platformLayerMask);
        RaycastHit2D raycastRight = Physics2D.Raycast(transform.position, Vector2.right, _hitWallRaycastDistance, _platformLayerMask);

        Debug.DrawRay(transform.position, Vector2.left * _hitWallRaycastDistance, raycastLeft ? Color.green : Color.red);
        Debug.DrawRay(transform.position, Vector2.right * _hitWallRaycastDistance, raycastRight ? Color.green : Color.red);

        if (raycastLeft || raycastRight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected bool CheckIfHittingGround()
    {
        RaycastHit2D raycastDownLeft = Physics2D.Raycast(transform.position + (Vector3.left * _hitGroundRaycastStartPosition), Vector2.down, _hitGroundRaycastDistance, _platformLayerMask);
        RaycastHit2D raycastDownRight = Physics2D.Raycast(transform.position + (Vector3.right * _hitGroundRaycastStartPosition), Vector2.down, _hitGroundRaycastDistance, _platformLayerMask);

        Debug.DrawRay(transform.position + (Vector3.left * _hitGroundRaycastStartPosition), Vector2.down * _hitGroundRaycastDistance, raycastDownLeft ? Color.green : Color.red);
        Debug.DrawRay(transform.position + (Vector3.right * _hitGroundRaycastStartPosition), Vector2.down * _hitGroundRaycastDistance, raycastDownRight ? Color.green : Color.red);

        if (!raycastDownLeft || !raycastDownRight)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    protected PlayerHealth CheckIfHittingPlayer()
    {
        RaycastHit2D raycastLeft = Physics2D.Raycast(transform.position, Vector2.left, _hitWallRaycastDistance, _playerLayerMask);
        RaycastHit2D raycastRight = Physics2D.Raycast(transform.position, Vector2.right, _hitWallRaycastDistance, _playerLayerMask);

        Debug.DrawRay(transform.position, Vector2.left * _hitWallRaycastDistance, raycastLeft ? Color.green : Color.red);
        Debug.DrawRay(transform.position, Vector2.right * _hitWallRaycastDistance, raycastRight ? Color.green : Color.red);

        if (raycastLeft)
        {
            return raycastLeft.collider.GetComponent<PlayerHealth>();
        }
        else if (raycastRight)
        {
            return raycastRight.collider.GetComponent<PlayerHealth>();
        }
        else
        {
            return null;
        }
    }
}
