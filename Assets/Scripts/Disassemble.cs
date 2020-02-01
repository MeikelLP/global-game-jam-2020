using System;
using System.Security.Cryptography;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Disassemble : MonoBehaviour
{

    [SerializeField] private InventoryScript inventory;

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();

        if(!_camera) throw new ArgumentNullException(nameof(_camera));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out var hit))
            {
                if (hit.collider.gameObject.TryGetComponent<PhonePart>(out var part))
                {
                    inventory.AddItemToInventory(part);
                }
            }
        }
    }
}