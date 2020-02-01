using System;
using UnityEngine;

public class FocusCameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 10;
    private Vector3 rot;

    private void Start()
    {
        if (!target) throw new ArgumentNullException("Need target");
    }

    private void Update()
    {
        if (!Input.GetMouseButton(1)) return;

        target.Rotate(Vector3.up + transform.forward, Input.GetAxis("Mouse X") * speed);
        target.Rotate(Vector3.left + transform.forward, Input.GetAxis("Mouse Y") * speed);
    }
}
