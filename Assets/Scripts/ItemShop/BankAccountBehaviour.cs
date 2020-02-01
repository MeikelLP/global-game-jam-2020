using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAccountBehaviour : MonoBehaviour
{
    public float money;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public bool Debit(float value)
    {
        float temp = money;
        money = money - value;
        if (money < 0)
        {
            money = temp;
            return false;
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
