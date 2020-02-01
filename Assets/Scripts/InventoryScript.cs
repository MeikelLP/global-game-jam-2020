using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public float firstItemX = 1f;
    public float firstItemY = 0.6f;
    public float firstItemZ = 1f;

    public float xPositionChange = 0.2f;
    public float yPositionChange = 1.0f;

    public Inventory inventory = new Inventory();
    public GameObject inventoryGameObject;

    private void Start()
    {
        inventoryGameObject = GameObject.Find("Inventory");
    }

    public void AddItemToInventory(PhonePart phonePart)
    {
        var item = phonePart.gameObject;
        var vector2 = inventory.AddItem(item);
        if (vector2 == null)
        {
            Destroy(item);
            // inventory full
            return;
        }

        var x = firstItemX + vector2.Value.x * xPositionChange;
        var y = firstItemY + vector2.Value.y * yPositionChange;
        var z = firstItemZ;

        var position = new Vector3(x, y, z);

        item.transform.parent = inventoryGameObject.transform;
        item.transform.localPosition = position;
        item.transform.localRotation = Quaternion.identity;
    }

    public void RemoveItemFromInventory(GameObject o)
    {
        var removedItem = inventory.RemoveItem(o);
        if (removedItem)
        {
            Destroy(removedItem);
        }
    }
}
