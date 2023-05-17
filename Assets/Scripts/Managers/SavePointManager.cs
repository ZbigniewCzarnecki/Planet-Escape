using System;
using System.Collections;
using UnityEngine;

public class SavePointManager : MonoBehaviour
{
    public static SavePointManager Instance { get; private set; }

    public event EventHandler OnPlayerSpawnOnCurrentSavePoint;

    [SerializeField] private GameObject _player;
    [SerializeField] private float _spawnOnCurrentSavePointTime = 0.4f;

    Vector2 _currentSavePointPosition;
    Vector2 _startPosition;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one SavePointManager! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _startPosition = _player.transform.position;
        _currentSavePointPosition = _startPosition;
    }

    private void Start()
    {
        PlayerHealth.Instance.OnDecreaseHealth += PlayerHealth_OnDecreaseHealth;
    }

    private void PlayerHealth_OnDecreaseHealth(object sender, EventArgs e)
    {
        StartCoroutine(SpawnPlayerOnCurrentSavePoint());
    }

    private IEnumerator SpawnPlayerOnCurrentSavePoint()
    {
        yield return new WaitForSeconds(_spawnOnCurrentSavePointTime);
        _player.transform.position = _currentSavePointPosition;

        yield return new WaitForSeconds(_spawnOnCurrentSavePointTime);
        OnPlayerSpawnOnCurrentSavePoint?.Invoke(this, EventArgs.Empty);
    }

    public void SetNewSavePoint(Vector2 savePointPosition)
    {
        _currentSavePointPosition = savePointPosition;
    }
}
