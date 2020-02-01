using System;
using Boo.Lang;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public float initialX = -0.4f;
    public float initialZ = -0.4f;

    public float positionChange = 0.2f;

    readonly List<GameObject> _inventoryItems = new List<GameObject>();
    public GameObject _inventory;

    private void Start()
    {
        _inventory = GameObject.Find("Inventory");
    }

    void Update()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     Debug.Log("Left mouse clicked");
        //     AddItemToInventory(null);
        // }
        //
        // if (Input.GetMouseButtonDown(1))
        // {
        //     Debug.Log("Right mouse clicked");
        //     Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
        //     RaycastHit hit;
        //
        //     if( Physics.Raycast( ray, out hit, 100 ) )
        //     {
        //         Debug.Log("hit something");
        //         if (hit.transform.gameObject.TryGetComponent<PhonePart>(out var _))
        //         {
        //             RemoveItemFromInventory(hit.transform.gameObject);
        //         }
        //     }
        //     else
        //     {
        //         Debug.Log("No hit");
        //     }
        // }
    }


    public void AddItemToInventory(PhonePart phonePart)
    {
        var currentNumberOfItems = _inventoryItems.Count;
        float x = initialX + (currentNumberOfItems * positionChange);
        float y = 1;
        float z = 0.4f;

        var position = new Vector3(x, y, z);
        // GameObject item = CreateItem(position);
        MoveItem(position, phonePart.gameObject);

        // TODO consider deletion of items for position

        _inventoryItems.Add(phonePart.gameObject);
    }

    private void RemoveItemFromInventory(GameObject o)
    {
        _inventoryItems.Remove(o);
        Destroy(o);
    }

    private GameObject CreateItem(Vector3 position)
    {
        //spawn object
        GameObject item = GameObject.CreatePrimitive(PrimitiveType.Cube);

        MoveItem(position, item);

        item.AddComponent<PhonePart>();

        return item;
    }

    private void MoveItem(Vector3 position, GameObject item)
    {
        item.transform.parent = _inventory.transform;
        item.transform.localPosition = position;
        item.transform.localRotation = Quaternion.identity;
    }
}
