using UnityEngine;

public class InventoryScript : MonoBehaviour
{
    public float xPositionChange = 0.2f;
    public float yPositionChange = 5.0f;

    public Inventory inventory = new Inventory();
    public GameObject inventoryGameObject;

    private void Start()
    {
      inventoryGameObject = GameObject.Find("Inventory");
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
        //
        //     if( Physics.Raycast( ray, out var hit, 100 ) )
        //     {
        //         if (hit.transform.gameObject.TryGetComponent<PhonePart>(out var _))
        //         {
        //             RemoveItemFromInventory(hit.transform.gameObject);
        //         }
        //     }
        // }
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

        var x = vector2.Value.x * xPositionChange;
        var y = vector2.Value.y * yPositionChange;
        var z = 1f;

        var position = new Vector3(x, y, z);

        item.transform.parent = inventoryGameObject.transform;
        item.transform.localPosition = position;
        item.transform.localRotation = Quaternion.identity;
    }

    private void RemoveItemFromInventory(GameObject o)
    {
        GameObject removedItem = inventory.RemoveItem(o);
        if (removedItem)
        {
            Destroy(removedItem);
        }
    }
}
