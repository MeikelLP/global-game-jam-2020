using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class ShopManagerBehaviour : MonoBehaviour
{
    public BankAccountBehaviour account;

    public InventoryScript inventory;

    public GameObject contentPage;

    public Phone[] phones;

    [HideInInspector] public List<PhonePart> phoneComponentList;

    public bool IsListFilled { get; private set; }

    private void FillComponentList()
    {
        for (int i = 0; i < phones.Length; i++)
        {
            if (phones[i])
            {
                StartCoroutine(WaitForPhoneInit(phones[i]));
            }
        }
        IsListFilled = true;
    }

    private IEnumerator WaitForPhoneInit(Phone phone)
    {
        yield return new WaitUntil(() => phone.initialized);
        AddComponentsFromPhoneToList(phone);
    }

    private void AddComponentsFromPhoneToList(Phone phone)
    {
        foreach (var phonePart in phone.parts)
        {
            if (phonePart)
            {
                phoneComponentList.Add(phonePart);
            }
        }
    }

    public void BuyComponent(PhonePart part)
    {
        if (part)
        {
            if (account.Debit(part.price))
            {
                inventory.Add(Instantiate(part));
                Debug.Log("Bought: " + part.title + "for" + part.price + " $!");
            }
        }
    }
    
    public void Start()
    {
        phoneComponentList = new List<PhonePart>();
        FillComponentList();
    }
    
}
