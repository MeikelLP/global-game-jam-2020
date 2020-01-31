using System;
using UnityEngine;

public class FocusCameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 10;

    private void Start()
    {
        if (!target) throw new ArgumentNullException("Need target");
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0)) return;

        var r = target.rotation.eulerAngles;
        var y = Input.GetAxis("Mouse Y") * speed;
        var x = Input.GetAxis("Mouse X") * speed;
        target.rotation = Quaternion.Euler(r.x + y, r.y - x, r.z);
    }
}
