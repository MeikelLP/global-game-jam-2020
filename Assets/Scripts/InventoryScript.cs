using System;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public float xPositionChange = 0.2f;
    public float yPositionChange = 5.0f;

    public Inventory inventory = new Inventory();
    public GameObject inventoryGameObject;

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
            // inventory full
            Debug.Log("INVENTORY FULL"); // TODO to UI message
            return;
        }

        phonePart.Assembled = false;

        var x = vector2.Value.x * xPositionChange;
        var y = vector2.Value.y * yPositionChange;
        var z = 1f;

        var position = new Vector3(x, y, z);

        item.transform.parent = inventoryGameObject.transform;
        item.transform.localPosition = position;
        item.transform.localRotation = Quaternion.identity;
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
            t.parent.SetParent(PhoneSelection.Instance.phone, false);
            t.localPosition = phonePart.originalLocalPosition;
            t.localRotation = phonePart.originalLocalRotation;
            phonePart.Assembled = true;
        }
    }
}
