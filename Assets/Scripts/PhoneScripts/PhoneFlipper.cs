using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PhoneFlipper : MonoBehaviour
{
    [SerializeField] private Vector3 frontRotation;
    [SerializeField] private Vector3 backRotation;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private KeyCode key = KeyCode.E;
    private bool IsFront => ActivePhoneTransform.transform.eulerAngles == frontRotation;

    public Transform ActivePhoneTransform  { get; set; }

    private void Start()
    {
        if(!ActivePhoneTransform) throw new NullReferenceException(nameof(ActivePhoneTransform));
        if(!infoText) throw new NullReferenceException(nameof(infoText));

        ActivePhoneTransform.rotation = Quaternion.Euler(backRotation);
        FlipPhone(); // ensure front always first
        infoText.text = key.ToString();
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
        ActivePhoneTransform.rotation = Quaternion.Euler(newRotation);
    }
}
