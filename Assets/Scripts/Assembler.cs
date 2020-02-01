using System;
using System.Security.Cryptography;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Assembler : MonoBehaviour
{
    [SerializeField] private InventoryScript inventory;
    [SerializeField] private new Camera camera;
    [SerializeField] private KeyCode key = KeyCode.Mouse0;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Color assembleColor = Color.green;
    [SerializeField] private Color disassembleColor = Color.red;
    [SerializeField] private SVGImage icon;


    private void Start()
    {
        if (!camera) throw new ArgumentNullException(nameof(camera));
        if (!inventory) throw new ArgumentNullException(nameof(inventory));
        if (!text) throw new ArgumentNullException(nameof(text));

        text.text = $"Select (<color=yellow>{key}</color>)";
    }

    private void LateUpdate()
    {
        icon.gameObject.SetActive(false);
        Cursor.visible = true;
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            if (hit.collider.gameObject.TryGetComponent<PhonePart>(out var part))
            {
                var toolMode = PhoneSelection.Instance.toolMode;
                var color = toolMode == ToolMode.Assemble ? assembleColor : disassembleColor;

                icon.gameObject.SetActive(true);
                icon.color = color;
                icon.rectTransform.position = camera.WorldToScreenPoint(hit.point);
                Cursor.visible = false;

                if (Input.GetKeyDown(key))
                {
                    if (toolMode == ToolMode.Assemble)
                    {
                        inventory.Remove(part);
                    }
                    else if (toolMode == ToolMode.Disassemble)
                    {
                        inventory.Add(part);
                    }
                }
            }
        }
    }

    private void OnDestroy()
    {
        icon.gameObject.SetActive(false);
        Cursor.visible = true;
    }
}
