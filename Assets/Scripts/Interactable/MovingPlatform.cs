using UnityEngine;
using UnityEngine.Events;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D _rb2d;

    [SerializeField] private UnityEvent OnCollisionEnterEvent;
    [SerializeField] private UnityEvent OnCollisionExitEvent;

    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private bool _isActive;

    [SerializeField] private Vector3[] _pathPointArray;
    private Vector3 _startPoint;
    private int _currentWaypointIndex = 0;

    [SerializeField] private LayerMask _collisionLayerMask;
    [SerializeField] private MovingPlatformTrigger _movingPlatformTrigger;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        _startPoint = transform.position;
    }

    void FixedUpdate()
    {
        if (_isActive)
        {
            _rb2d.velocity = GetTargetPointPositionNormalized() * _moveSpeed;
        }
        else
        {
            _rb2d.velocity = Vector2.zero;
            return;
        }

        //Platform Movement
        if (Vector2.Distance(transform.position, (_startPoint + _pathPointArray[_currentWaypointIndex])) < 0.1f)
        {
            _currentWaypointIndex = (_currentWaypointIndex + 1) % _pathPointArray.Length;

            _rb2d.velocity = Vector2.zero;
            _rb2d.velocity = GetTargetPointPositionNormalized() * _moveSpeed;

            _movingPlatformTrigger.SetPlatformVelocityX(_rb2d.velocity.x);
            _movingPlatformTrigger.SetPlatformVelocityY(_rb2d.velocity.y);
        }
    }

    private Vector3 GetTargetPointPositionNormalized()
    {
        return ((_startPoint + _pathPointArray[_currentWaypointIndex]) - transform.position).normalized;
    }

    public void ActivatePlatform(bool isActive)
    {
        _isActive = isActive;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_collisionLayerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            OnCollisionEnterEvent?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if ((_collisionLayerMask.value & (1 << collision.transform.gameObject.layer)) > 0)
        {
            OnCollisionExitEvent?.Invoke();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int i = 0; i < _pathPointArray.Length; i++)
        {
            if (!Application.isPlaying)
                Gizmos.DrawSphere(transform.position + _pathPointArray[i], 0.2f);
            else
                Gizmos.DrawSphere(_startPoint + _pathPointArray[i], 0.2f);
        }
    }
}
