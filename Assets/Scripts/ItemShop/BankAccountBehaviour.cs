using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankAccountBehaviour : MonoBehaviour
{
    [SerializeField] private float _money;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    public bool Debit(float value)
    {
        float temp = _money;
        _money -= value;
        if (_money < 0)
        {
            _money = temp;
            return false;
        }

        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
