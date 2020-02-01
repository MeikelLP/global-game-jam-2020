using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneFlipper : MonoBehaviour
{
    [SerializeField] private Vector3 frontRotation;
    [SerializeField] private Vector3 backRotation;
    [SerializeField] private Transform phone;
    [SerializeField] private Text infoText;
    [SerializeField] private KeyCode key = KeyCode.E;
    private bool IsFront => phone.transform.eulerAngles == frontRotation;

    private void Start()
    {
        if(!phone) throw new NullReferenceException(nameof(phone));
        if(!infoText) throw new NullReferenceException(nameof(infoText));

        phone.transform.rotation = Quaternion.Euler(backRotation);
        FlipPhone(); // ensure front always first
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            FlipPhone();
        }
    }

    private void FlipPhone()
    {
        var newRotation = IsFront ? backRotation : frontRotation;
        phone.transform.rotation = Quaternion.Euler(newRotation);
        infoText.text = $"{(IsFront ? "Front" : "Back")} (<color=yellow>{key}</color>)";
    }
}
