using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private const string IS_GROUNDED = "IsGrounded";
    private const string IS_MOVING = "IsMoving";
    private const string IS_FALLING = "IsFalling";
    private const string EXPLODE = "Explode";
    private const string SPAWN = "Spawn";

    public static event EventHandler OnStepOnGroundEvent;

    [SerializeField] private PlayerMovement _playerMovement;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        PlayerHealth.Instance.OnDecreaseHealth += PlayerHealth_OnDecreaseHealth;
        SavePointManager.Instance.OnPlayerSpawnOnCurrentSavePoint += SavePointManager_OnPlayerSpawnOnCurrentSavePoint;
    }

    private void Update()
    {
        SetIsMovingBool();
        SetIsGroundedBool();
        SetFallingTrigger();
    }

    private void SetIsMovingBool()
    {
        _animator.SetBool(IS_MOVING, _playerMovement.IsMoving());
    }

    private void SetIsGroundedBool()
    {
        _animator.SetBool(IS_GROUNDED, _playerMovement.IsGrounded());
    }

    private void SetFallingTrigger()
    {
        _animator.SetBool(IS_FALLING, _playerMovement.IsFalling());
    }

    private void PlayerHealth_OnDecreaseHealth(object sender, EventArgs e)
    {
        _animator.SetTrigger(EXPLODE);
    }

    private void SavePointManager_OnPlayerSpawnOnCurrentSavePoint(object sender, EventArgs e)
    {
        _animator.Play(SPAWN);
    }

    public void PlayWalkSound()
    {
        OnStepOnGroundEvent?.Invoke(this, EventArgs.Empty);
    }
}
