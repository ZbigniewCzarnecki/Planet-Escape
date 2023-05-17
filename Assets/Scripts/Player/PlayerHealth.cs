using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance { get; private set; }

    public event EventHandler OnDecreaseHealth;
    public event EventHandler OnIncreaseHealth;
    public event EventHandler OnDeath;

    private readonly int _healthMax = 3;
    private int _currentHealth;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one PlayerHealth! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _currentHealth = _healthMax;
    }

    public void DecreaseHealth()
    {
        _currentHealth--;

        if (_currentHealth <= 0)
        {
            OnDeath?.Invoke(this, EventArgs.Empty);
        }

        AudioManager.Instance.PlayPlayerHurtSound();

        OnDecreaseHealth?.Invoke(this, EventArgs.Empty);
    }

    public void IncreaseHealth()
    {
        if (_currentHealth < _healthMax)
        {
            _currentHealth++;

            OnIncreaseHealth?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetCurrentHealth()
    {
        return _currentHealth;
    }
}
