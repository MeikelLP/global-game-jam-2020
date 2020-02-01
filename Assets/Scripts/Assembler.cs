using System;
using System.Security.Cryptography;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class Assembler : MonoBehaviour
{
    [SerializeField] private InventoryScript inventory;
    [SerializeField] private new Camera camera;
    [SerializeField] private KeyCode key = KeyCode.Mouse0;
    [SerializeField] private TextMeshProUGUI text;

    private void Start()
    {
        if(!camera) throw new ArgumentNullException(nameof(camera));
        if(!inventory) throw new ArgumentNullException(nameof(inventory));
        if(!text) throw new ArgumentNullException(nameof(text));

        text.text = $"Select (<color=yellow>{key}</color>)";
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
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
