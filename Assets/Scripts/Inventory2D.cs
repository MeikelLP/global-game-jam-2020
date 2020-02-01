using UnityEngine;
using JetBrains.Annotations;

public class Inventory2D
{
    private const int ItemsPerRow = 5;
    private const int NumberOfRows = 5;

    readonly GameObject[,] _inventoryItems = new GameObject[ItemsPerRow, NumberOfRows];

    /// <summary>
    /// Returns the position of the added item.
    /// Returns null if inventory is full
    /// </summary>
    /// <param name="phonePart"></param>
    /// <returns></returns>
    public Vector2? AddItem(GameObject phonePart)
    {
        for (var y = 0; y < ItemsPerRow; y++)
        {
            for (var x = 0; x < NumberOfRows; x++)
            {
                if (_inventoryItems[x, y] == null)
                {
                    _inventoryItems[x, y] = phonePart;
                    return new Vector2(x, y);
                }
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
        for (var y = 0; y < ItemsPerRow; y++)
        {
            for (var x = 0; x < NumberOfRows; x++)
            {
                if (_inventoryItems[x, y] == phonePart)
                {
                    _inventoryItems[x, y] = null;
                    return phonePart;
                }
            }
        }
        return null;
    }
}
