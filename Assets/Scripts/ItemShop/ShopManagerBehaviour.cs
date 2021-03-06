﻿using System.Collections.Generic;
using UnityEngine;

public class ShopManagerBehaviour : MonoBehaviour
{
    public BankAccountBehaviour account;
    public ItemShopBehaviour shop;
    [HideInInspector] public List<PhonePart> phoneComponentList;
    private InventoryScript _inventory;
    public KeyCode _open;
    
    void Update()
    {
        if (Input.GetKeyDown(_open))
        {
            if (shop.gameObject.activeSelf)
            {
                shop.gameObject.SetActive(false);
            }
            else
            {
                shop.gameObject.SetActive(true);
            }
        }
    }
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
                var cloned = Instantiate(part);
                cloned.Phone = part.Phone;
                cloned.broken = false;
                _inventory.Add(cloned);
                Debug.Log($"Bought: {part} for {part.price} $!");
            }
        }
    }

    public void Initialize(Phone phone)
    {
        shop.gameObject.SetActive(false);
        phoneComponentList = new List<PhonePart>();
        FillComponentList(phone);
        shop.Initialize(phone);

        _inventory = FindObjectOfType<InventoryScript>();
    }
}
