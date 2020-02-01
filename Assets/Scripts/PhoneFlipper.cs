using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneFlipper : MonoBehaviour
{
    [SerializeField] private Vector3 frontRotation;
    [SerializeField] private Vector3 backRotation;
    [SerializeField] private Transform phone;

    private void Start()
    {
        phone.transform.rotation = Quaternion.Euler(frontRotation);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var newRotation = phone.transform.eulerAngles == frontRotation ? backRotation : frontRotation;
            phone.transform.rotation = Quaternion.Euler(newRotation);
        }
    }
}
