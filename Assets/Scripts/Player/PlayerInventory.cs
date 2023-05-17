using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private Dictionary<string, Sprite> _inventory = new();

    public void AddInventoryItem(string itemName, Sprite itemSprite)
    {
        _inventory.Add(itemName, itemSprite);
    }

    public bool GetInventoryItem(string name)
    {
        return _inventory.ContainsKey(name);
    }

    public void RemoveInventoryItem(string name)
    {
        _inventory.Remove(name);
    }
}
