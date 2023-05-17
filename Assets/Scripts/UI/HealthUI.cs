using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Sprite _fullHeartImage, _emptyHeartImage;

    [SerializeField] private Transform _heartsContainer;

    private readonly List<Image> _heartsList = new();

    private void Awake()
    {
        foreach (Transform child in _heartsContainer)
        {
            _heartsList.Add(child.GetComponent<Image>());
            _heartsList[^1].sprite = _fullHeartImage;
        }

        UpdateHealthUI();
    }

    private void Start()
    {
        PlayerHealth.Instance.OnIncreaseHealth += PlayerHealth_OnIncreaseHealth;
        PlayerHealth.Instance.OnDecreaseHealth += PlayerHealth_OnDecreaseHealth;
    }

    private void PlayerHealth_OnIncreaseHealth(object sender, System.EventArgs e)
    {
        UpdateHealthUI();
    }

    private void PlayerHealth_OnDecreaseHealth(object sender, EventArgs e)
    {
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        for (int i = 0; i < _heartsList.Count; i++)
        {
            if (i < PlayerHealth.Instance.GetCurrentHealth())
            {
                _heartsList[i].sprite = _fullHeartImage;
            }
            else
            {
                _heartsList[i].sprite = _emptyHeartImage;
            }
        }
    }
}
