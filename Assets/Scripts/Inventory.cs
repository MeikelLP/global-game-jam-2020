using UnityEngine;
using JetBrains.Annotations;

public class Inventory
{
    private const int ItemsPerRow = 15;
    public const float PositionY = 1f;

    private readonly GameObject[] _inventoryItems = new GameObject[ItemsPerRow];

    /// <summary>
    /// Returns the position of the added item.
    /// Returns null if inventory is full
    /// </summary>
    /// <param name="phonePart"></param>
    /// <returns></returns>
    public Vector2? AddItem(GameObject phonePart)
    {
        for (var x = 0; x < ItemsPerRow; x++)
        {
            if (_inventoryItems[x] == null)
            {
                _inventoryItems[x] = phonePart;
                return new Vector2(x, PositionY);
            }
        }

        return null;
    }

    /// <summary>
    /// Returns the removed element or null if none is found
    /// </summary>
    /// <param name="phonePart"></param>
    /// <returns></returns>
    [CanBeNull]
    public GameObject RemoveItem(GameObject phonePart)
    {
        for (var x = 0; x < ItemsPerRow; x++)
        {
            if (_inventoryItems[x] == phonePart)
            {
                _inventoryItems[x] = null;
                return phonePart;
            }
        }
        return null;
    }
}
