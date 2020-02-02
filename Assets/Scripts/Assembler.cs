using System;
using DefaultNamespace;
using GameScripts;
using UnityEngine;
using UnityEngine.UI;

public class Assembler : MonoBehaviour
{
    [SerializeField] private InventoryScript inventory;
    [SerializeField] private DebugView debugView;
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

    public GameState gameState;

    private void Start()
    {
        if (!camera) throw new ArgumentNullException(nameof(camera));
        if (!inventory) throw new ArgumentNullException(nameof(inventory));
        if (!gameState) throw new ArgumentNullException(nameof(gameState));
        if (!debugView) throw new ArgumentNullException(nameof(debugView));

        progressIcon.fillAmount = 0;
        _isReleased = true;
    }

    private void LateUpdate()
    {
        hoverIcon.gameObject.SetActive(false);
        Cursor.visible = true;
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            if (hit.collider.gameObject.TryGetComponent<PhonePart>(out var part))
            {
                var toolMode = Toolbar.Instance.toolMode;
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
                }
                else if (Input.GetKey(key) && _isReleased && part == _selected)
                {
                    progressIcon.rectTransform.position = iconPosition;
                    _progress += Time.deltaTime / timeToHold;
                    _progress = Mathf.Clamp(_progress, 0, 1);

                    if (_progress >= 1)
                    {
                        _isReleased = false;
                        _progress = 0;
                        switch (toolMode)
                        {
                            case ToolMode.Assemble when !part.Assemblable:
                                // TODO show ui message that part can not be assembled
                                Debug.Log("Item can not be assembled");
                                break;
                            case ToolMode.Assemble:
                                inventory.Remove(part);
                                part.Phone.AddPart(part);
                                gameState.CheckPhone(part.Phone);
                                debugView.Refresh(part.Phone);
                                break;
                            case ToolMode.Disassemble when !part.Disassemblable:
                                Debug.Log("Item can not be disassembled");
                                // TODO show ui message that part can not be dissembled
                                break;
                            case ToolMode.Disassemble:
                                inventory.Add(part);
                                part.Phone.RemovePart(part);
                                debugView.Refresh(part.Phone);
                                break;
                        }
                    }
                }
            }
            else
            {
                _progress = 0;
            }
        }
        else
        {
            _progress = 0;
        }

        progressIcon.fillAmount = _progress;
    }

    private void OnDestroy()
    {
        hoverIcon.gameObject.SetActive(false);
        Cursor.visible = true;
    }
}
