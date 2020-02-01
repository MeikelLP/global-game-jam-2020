using System;
using System.Security.Cryptography;
using DefaultNamespace;
using UnityEngine;

public class Assembler : MonoBehaviour
{
    [SerializeField] private InventoryScript inventory;
    [SerializeField] private new Camera camera;

    private void Start()
    {
        if(!camera) throw new ArgumentNullException(nameof(camera));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit))
            {
                if (hit.collider.gameObject.TryGetComponent<PhonePart>(out var part))
                {
                    if (PhoneSelection.Instance.toolMode == ToolMode.Assemble)
                    {
                        inventory.Remove(part);
                    }
                    else if(PhoneSelection.Instance.toolMode == ToolMode.Disassemble)
                    {
                        inventory.Add(part);
                    }
                }
            }
        }
    }
}
