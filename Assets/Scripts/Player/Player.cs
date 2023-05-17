using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    private CapsuleCollider2D _capsuleCollider2D;
    private PlayerMovement _playerMovement;
    private Rigidbody2D _rb2d;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _playerMovement = GetComponent<PlayerMovement>();
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        PlayerHealth.Instance.OnDecreaseHealth += PlayerHealth_OnDecreaseHealth;
        SavePointManager.Instance.OnPlayerSpawnOnCurrentSavePoint += SavePointManager_OnPlayerSpawnOnCurrentSavePoint;
    }

    private void GameManager_OnGameOver(object sender, System.EventArgs e)
    {
        PlayerLimitations(false, true, RigidbodyType2D.Static);
    }

    private void PlayerHealth_OnDecreaseHealth(object sender, System.EventArgs e)
    {
        PlayerLimitations(false, true, RigidbodyType2D.Static);
    }

    private void SavePointManager_OnPlayerSpawnOnCurrentSavePoint(object sender, System.EventArgs e)
    {
        PlayerLimitations(true, false, RigidbodyType2D.Dynamic);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out IInteractable interactable))
        {
            interactable.InteractBegin(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.TryGetComponent(out IInteractable interactable))
        {
            interactable.InteractEnd();
        }
    }

    private void PlayerLimitations(bool colliderEnabled, bool preventMoves, RigidbodyType2D rb2dBodyType)
    {
        _capsuleCollider2D.enabled = colliderEnabled;
        _playerMovement.PreventFromMoving(preventMoves);
        _rb2d.bodyType = rb2dBodyType;
    }
}
