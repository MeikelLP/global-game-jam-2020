using System;
using PhoneScripts;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public float firstItemX = 0.1f;
    public float firstItemY = 1f;
    public float firstItemZ = 0f;

    public float xPositionChange = -0.05f;

    public float scaleFactor = 0.25f;
    
    public Inventory inventory = new Inventory();
    public GameObject inventoryGameObject;

    public Transform ActivePhoneTransform  { get; set; }
    
    private void Start()
    {
        if (!inventoryGameObject) throw new NullReferenceException(nameof(inventoryGameObject));
    }

    public void Add(PhonePart phonePart)
    {
        var item = phonePart.gameObject;
        var vector2 = inventory.AddItem(item);
        if (vector2 == null)
        {
            Destroy(item);
            // inventory full
            Debug.Log("INVENTORY FULL"); // TODO to UI message
            return;
        }

        phonePart.Assembled = false;
        item.transform.localScale = CalculateItemScale(item);
        item.transform.localPosition = CalculateItemPosition(vector2.Value);
        item.transform.localRotation = Quaternion.identity;
        item.transform.SetParent( inventoryGameObject.transform, false);
    }

    public void Remove(PhonePart part)
    {
        var removedItem = inventory.RemoveItem(part.gameObject);
        if (removedItem)
        {
            // ReSharper disable once PossibleNullReferenceException
            var phonePart = removedItem.GetComponent<PhonePart>();

            // reset pos
            var t = removedItem.transform;
            t.parent.SetParent(ActivePhoneTransform, false);
            t.localPosition = phonePart.OriginalLocalPosition;
            t.localRotation = phonePart.OriginalLocalRotation;
            phonePart.Assembled = true;
        }
    }

    private Vector3 CalculateItemPosition(Vector2 vector2)
    {
        var x = firstItemX + vector2.x * xPositionChange;
        var y = firstItemY;
        var z = firstItemZ;
        var position = new Vector3(x, y, z);
        return position;
    }

    private Vector3 CalculateItemScale(GameObject item)
    {
        var parentScale = inventoryGameObject.transform.localScale;
        var itemScale = item.transform.localScale;
        var newX = scaleFactor / (parentScale.x * itemScale.x);
        var newY = scaleFactor / (parentScale.y * itemScale.z);
        var newZ = scaleFactor / (parentScale.z * itemScale.z);
        
        return new Vector3(newX, newY, newZ);
    }
}
