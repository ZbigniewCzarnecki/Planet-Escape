using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance { get; private set; }

    public int UnlockedLevelIndex { get; set; }
    public int CoinsAmount { get; set; }
    public int DeathsAmount { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one SaveData! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }

        Instance = this;

        Load();
    }

    public void Save()
    {
        Data data = new()
        {
            unlockedLevelIndex = UnlockedLevelIndex,
            coinsAmount = CoinsAmount,
            deathsAmount = DeathsAmount,
        };

        string saveContent = JsonUtility.ToJson(data, true);

        SaveSystem.Save(saveContent);
    }

    public void Load()
    {
        string saveContent = SaveSystem.Load();

        if (saveContent != null)
        {
            Data data = JsonUtility.FromJson<Data>(saveContent);

            UnlockedLevelIndex = data.unlockedLevelIndex;
            CoinsAmount = data.coinsAmount;
            DeathsAmount = data.deathsAmount;
        }
        else
        {
            UnlockedLevelIndex = 1;
            CoinsAmount = 0;
            DeathsAmount = 0;
        }
    }

    public void ResetData()
    {
        UnlockedLevelIndex = 1;
        CoinsAmount = 0;
        DeathsAmount = 0;

        Save();
    }

    public class Data
    {
        public int unlockedLevelIndex;
        public int coinsAmount;
        public int deathsAmount;
    }
}
