using System;
using UnityEngine;

public class PhonePart : MonoBehaviour
{
    public Vector3 originalLocalPosition;
    public Quaternion originalLocalRotation;

    private void Start()
    {
        var t = transform;
        originalLocalPosition = t.localPosition;
        originalLocalRotation = t.localRotation;
    }
}
