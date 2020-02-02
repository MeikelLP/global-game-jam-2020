using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopBehaviour : MonoBehaviour
{
    private Text _keyInfoText;

    public KeyCode _open;

    public GameObject contentPage;

    private List<Image> _images;

    public ShopManagerBehaviour shopManager;

    public GameObject button;

    private CameraItemRenderer renderer;

    // Start is called before the first frame update
    public void Initialize(Phone phone)
    {
        _keyInfoText = GetComponentInChildren<Text>();
        _keyInfoText.text = $"[{_open}] Shop";
        contentPage.SetActive(false);
        renderer = FindObjectOfType<CameraItemRenderer>();

        FillShop(phone);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_open))
        {
            if (contentPage.activeSelf)
            {
                contentPage.SetActive(false);
                Debug.Log(shopManager.phoneComponentList.Count);
            }
            else
            {
                contentPage.SetActive(true);
            }
        }
    }

    private void FillShop(Phone phone)
    {
        foreach (var phonePart in phone.parts)
        {
            GameObject newButton = Instantiate(button, Vector3.zero, Quaternion.identity, contentPage.transform);
            newButton.GetComponent<Button>().onClick.AddListener(() => shopManager.BuyComponent(phonePart));
            Texture2D tex2d;
            if (!renderer.Images.TryGetValue(phonePart.ToString(), out tex2d) || !tex2d)
            {
                throw new Exception();
            }

            newButton.GetComponent<Image>().sprite = Sprite.Create(tex2d, new Rect(0, 0, tex2d.width, tex2d.height),
                new Vector2(tex2d.width / 2, tex2d.height / 2));
        }
    }
}
