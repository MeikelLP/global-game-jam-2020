using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ManagerBehaviour : MonoBehaviour
{
    public BankAccountBehaviour account;

    public InventoryScript inventory;

    public GameObject contentPage;

    public Phone[] phones;

    [HideInInspector] public Dictionary<int, PhonePart> phoneComponentDictionary;

    private void FillComponentDictionary()
    {
        int id = 0;
        for (int i = 0; i < phones.Length; i++)
        {
            if (phones[i])
            {
                for (int j = 0; j < phones[i].parts.Length; j++)
                {
                    if (phones[i].parts[j])
                    {
                        phoneComponentDictionary.Add(id,phones[i].parts[j]);
                        id++;   
                    }
                }
            }
        }
    }

    public void BuyComponent(int id)
    {
        
    }
    
    public void Start()
    {
        phoneComponentDictionary = new Dictionary<int, PhonePart>();
        FillComponentDictionary();
    }
    
}
