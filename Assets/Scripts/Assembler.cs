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
    [SerializeField] private Color assembleColor = Color.green;
    [SerializeField] private Color disassembleColor = Color.red;
    [SerializeField] private SVGImage hoverIcon;
    [SerializeField] private Image progressIcon;
    [SerializeField] private float timeToHold = 2;
    private float _progress;
    private bool _isReleased;
    private PhonePart _selected;


    private void Start()
    {
        if (!camera) throw new ArgumentNullException(nameof(camera));
        if (!inventory) throw new ArgumentNullException(nameof(inventory));

        progressIcon.fillAmount = 0;
        _isReleased = true;
    }

    private void LateUpdate()
    {
        hoverIcon.gameObject.SetActive(false);
        Cursor.visible = true;
        if (!Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            _progress = 0;
            // nothing hit
            return;
        }

        if (!hit.collider.gameObject.TryGetComponent<PhonePart>(out var part))
        {
            _progress = 0;
            // no phone part hit
            return;
        }

        var toolMode = PhoneSelection.Instance.toolMode;
        var color = toolMode == ToolMode.Assemble ? assembleColor : disassembleColor;
        var iconPosition = camera.WorldToScreenPoint(hit.point);

        hoverIcon.gameObject.SetActive(true);
        hoverIcon.color = color;
        hoverIcon.rectTransform.position = iconPosition;
        Cursor.visible = false;

        if (_selected != part)
        {
            _progress = 0;
            _selected = part;
        }

        if (Input.GetKeyUp(key))
        {
            _isReleased = true;
            _progress = 0;
        } else if (Input.GetKey(key) && _isReleased && part == _selected)
        {
            progressIcon.rectTransform.position = iconPosition;
            _progress += Time.deltaTime / timeToHold;
            _progress = Mathf.Clamp(_progress, 0, 1);

            if (_progress >= 1)
            {
                switch (toolMode)
                {
                    case ToolMode.Assemble when !part.Assemblable():
                        // TODO show ui message that part can not be assembled 
                        Debug.Log("Item can not be assembled");
                        return;
                    case ToolMode.Assemble:
                        inventory.Remove(part);
                        break;
                    case ToolMode.Disassemble when !part.Disassemblable():
                        Debug.Log("Item can not be disassembled");
                        // TODO show ui message that part can not be dissembled 
                        break;
                    case ToolMode.Disassemble:
                        inventory.Add(part);
                        break;
                }
            }
        }
        
        progressIcon.fillAmount = _progress;
    }

    private void OnDestroy()
    {
        hoverIcon.gameObject.SetActive(false);
        Cursor.visible = true;
    }
}
