using System;
using System.Collections;
using System.Collections.Generic;
using PhoneScripts;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class ShopManagerBehaviour : MonoBehaviour
{
    public BankAccountBehaviour account;

    public InventoryScript inventory;

    public GameObject contentPage;
    
    [HideInInspector] public List<PhonePart> phoneComponentList;

    public ItemShopBehaviour shop;

    public bool IsListFilled { get; private set; }

    private void FillComponentList(Phone phone)
    {
        AddComponentsFromPhoneToList(phone);
        IsListFilled = true;
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
                Debug.Log("Bought: " + part + " for " + part.price + " $!");
            }
        }
    }
    
    public void Initialize(Phone phone)
    {
        phoneComponentList = new List<PhonePart>();
        FillComponentList(phone);
        shop.Initialize(phone);
    }
    
}
