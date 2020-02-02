using System;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    public GameObject inventoryGameObject;
    public GameObject inventoryItemPrefab;
    public Transform container;

    public Transform ActivePhoneTransform { get; set; }

    private CameraItemRenderer _renderer;

    private void Start()
    {
        if (!inventoryGameObject) throw new NullReferenceException(nameof(inventoryGameObject));

        _renderer = FindObjectOfType<CameraItemRenderer>();

        if (!_renderer) throw new ArgumentNullException(nameof(_renderer));
    }

    public void Add(PhonePart phonePart)
    {
        phonePart.Assembled = false;
        phonePart.gameObject.SetActive(false);
        if (!_renderer.Images.TryGetValue(phonePart.ToString(), out var texture))
        {
            throw new Exception($"No image found for: {phonePart}");
        }

        var invItem = Instantiate(inventoryItemPrefab, container);
        invItem.GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height),
            new Vector2(0.5f, 0.5f));
        invItem.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = phonePart.ToString();
        invItem.GetComponent<Button>().onClick.AddListener(() =>
        {
            // ReSharper disable once Unity.NoNullPropagation
            if (Toolbar.Instance.activeTool?.GetType() == typeof(Dissassembler))
            {
                if (phonePart.Assemblable)
                {
                    Remove(phonePart);
                    Destroy(invItem);
                }
                else
                {
                    Debug.Log("Phone part requires others to be installed first.");
                }
            }
        });
    }

    public void Remove(PhonePart part)
    {
        part.Assembled = true;
        part.gameObject.SetActive(true);
    }
}
