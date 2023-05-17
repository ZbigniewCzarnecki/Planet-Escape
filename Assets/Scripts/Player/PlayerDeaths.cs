using UnityEngine;

public class PlayerDeaths : MonoBehaviour
{
    private int _deaths;

    private void Start()
    {
        PlayerHealth.Instance.OnDecreaseHealth += PlayerHealth_OnDecreaseHealth;

        _deaths = SaveData.Instance.DeathsAmount;
    }

    private void PlayerHealth_OnDecreaseHealth(object sender, System.EventArgs e)
    {
        AddDeath();
        SaveDeathsAmount();
    }

    public void AddDeath()
    {
        _deaths++;
    }

    public void SaveDeathsAmount()
    {
        if (_deaths > SaveData.Instance.DeathsAmount)
        {
            SaveData.Instance.DeathsAmount = _deaths;
            SaveData.Instance.Save();
        }
    }
}
