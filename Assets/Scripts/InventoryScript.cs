using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public float firstItemX = -0.2f;
    public float firstItemY = 0.5f;
    public float firstItemZ = 1f;

    public float xPositionChange = 0.2f;
    public float yPositionChange = 1f;

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

        item.transform.localScale = CalculateItemScale(item);
        item.transform.localPosition = CalculateItemPosition(vector2.Value);
        item.transform.localRotation = Quaternion.identity;
        item.transform.SetParent( inventoryGameObject.transform, false);
    }

    public void RemoveItemFromInventory(GameObject o)
    {
        var removedItem = inventory.RemoveItem(o);
        if (removedItem)
        {
            Destroy(removedItem);
        }
    }

    private Vector3 CalculateItemPosition(Vector2 vector2)
    {
        var x = firstItemX + vector2.x * xPositionChange;
        var y = firstItemY + vector2.y * yPositionChange;
        var z = firstItemZ;
        var position = new Vector3(x, y, z);
        return position;
    }

    private Vector3 CalculateItemScale(GameObject item)
    {
        var parentScale = inventoryGameObject.transform.localScale;
        var itemScale = item.transform.localScale;
        var newX = 1 / (parentScale.x * itemScale.x);
        var newY = 1 / (parentScale.y * itemScale.z);
        var newZ = 1 / (parentScale.z * itemScale.z);
        
        return new Vector3(newX, newY, newZ);
    }
}
