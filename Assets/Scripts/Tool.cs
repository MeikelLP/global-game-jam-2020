using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class Tool : MonoBehaviour
{
    [SerializeField] private float timeToHold = 0.5f;
    [SerializeField] private KeyCode key = KeyCode.Mouse0;
    [SerializeField] private new Camera camera;
    [SerializeField] private Image progressIcon;
    public SVGImage hoverIcon;

    private float _progress;
    private bool _isReleased;
    private PhonePart _selected;

    public Phone Phone { get; set; }

    protected virtual void OnEnable()
    {
        if (!camera) throw new ArgumentNullException(nameof(camera));

        progressIcon.fillAmount = 0;
        _isReleased = true;
    }

    private void OnDisable()
    {
        hoverIcon.enabled = false;
        Cursor.visible = true;
    }

    private void LateUpdate()
    {
        hoverIcon.enabled = false;
        Cursor.visible = true;
        if (Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out var hit))
        {
            if (hit.collider.gameObject.TryGetComponent<PhonePart>(out var part))
            {
                var iconPosition = camera.WorldToScreenPoint(hit.point);

                hoverIcon.enabled = true;
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

                        OnInteract(part);
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

    protected virtual void OnInteract(PhonePart part)
    {
    }
}
