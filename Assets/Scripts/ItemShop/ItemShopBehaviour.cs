using System.Collections;
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

    public CameraItemRenderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        _keyInfoText = GetComponentInChildren<Text>();
        _keyInfoText.text = "[" + _open.ToString() + "] Shop";
        contentPage.SetActive(false);
        StartCoroutine(WaitForFilledList());
        StartCoroutine(WaitForOneSecondToFillShop());
    }

    private IEnumerator WaitForFilledList()
    {
        yield return new WaitUntil(() => shopManager.IsListFilled);
        RenderPhones();
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

    private void RenderPhones()
    {
        foreach (var phone in shopManager.phones)
        {
            if (phone)
            {
                StartCoroutine(WaitForPhoneInit(phone));
            }
        }
    }

    private IEnumerator WaitForPhoneInit(Phone phone)
    {
        yield return new WaitUntil(() => phone.initialized);
        StartCoroutine(renderer.StartRendering(phone));
    }

    private IEnumerator WaitForOneSecondToFillShop()
    {
        yield return new WaitForSeconds(1);
        FillShop();
    }

    private void FillShop()
    {
        Debug.Log(shopManager.phoneComponentList.Count);
        foreach (var phonePart in shopManager.phoneComponentList)
        {
            GameObject newButton = Instantiate(button,Vector3.zero,Quaternion.identity,contentPage.transform);
            newButton.GetComponent<Button>().onClick.AddListener((() => shopManager.BuyComponent(phonePart)));
            newButton.transform.SetParent(contentPage.transform);
            Texture2D tex2d;
            if (renderer.Images.TryGetValue(phonePart.ToString(), out tex2d))
            {
                newButton.GetComponent<Image>().sprite = Sprite.Create(tex2d,new Rect(0,0,tex2d.width,tex2d.height),new Vector2(0.5f,0.5f) );
            }
            else
            {
                Debug.Log("Phone Component " + phonePart.ToString() + " not rendered!");
            }
        }
    }
}
