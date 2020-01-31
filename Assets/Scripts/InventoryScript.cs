using System;
using Boo.Lang;
using UnityEngine;

namespace UnityTemplateProjects
{
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
            if (Input.GetMouseButtonDown(0))
            {
                MoveItemToInventory(null);
            }
        }

        public void MoveItemToInventory(Item phoneComponent)
        {
            var currentNumberOfItems = _inventoryItems.Count;
            float x = initialX + (currentNumberOfItems * positionChange);
            float y = 1;
            float z = 0.4f;

            var position = new Vector3(x, y, z);
            Debug.Log("Creating cube with position" + position);
            
            // TODO set position depending on the number items in the inventory
            
            GameObject item = CreateItem(position);
            
            _inventoryItems.Add(item);
        }

        private GameObject CreateItem(Vector3 position)
        {
            //spawn object
            GameObject item = GameObject.CreatePrimitive(PrimitiveType.Cube);
            
            item.transform.parent = _inventory.transform;
            item.transform.localPosition = position;
            
            return item;
        }
    }

    public class Item
    {
        private String name;
    }
}