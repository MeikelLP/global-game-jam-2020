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

    // Start is called before the first frame update
    void Start()
    {
        _keyInfoText = GetComponentInChildren<Text>();
        _keyInfoText.text = "[" + _open.ToString() + "] Shop";
        contentPage.SetActive(false);
        StartCoroutine(WaitForFillShop());
    }

    private IEnumerator WaitForFillShop()
    {
        yield return new WaitUntil(() => shopManager.isListFilled);
        FillShop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_open))
        {
            if (contentPage.activeSelf)
            {
                contentPage.SetActive(false);
            }
            else
            {
                contentPage.SetActive(true);
            }
        }
    }

    private void FillShop()
    {
        foreach (var phonePart in shopManager.phoneComponentList)
        {
            GameObject newButton = Instantiate(button,Vector3.zero,Quaternion.identity,contentPage.transform);
            newButton.GetComponent<Button>().onClick.AddListener((() => shopManager.BuyComponent(phonePart)));
            newButton.transform.SetParent(contentPage.transform);
        }

        Debug.Log("shop filled!: " + shopManager.phoneComponentList.Count);
    }
}
