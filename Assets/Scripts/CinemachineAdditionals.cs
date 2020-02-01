using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera), typeof(CinemachineFollowZoom))]
public class CinemachineAdditionals : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 0.1f;

    private CinemachineOrbitalTransposer _transposer;
    private CinemachineFollowZoom _zoom;

    private void Start()
    {
        var cam = GetComponent<CinemachineVirtualCamera>();
        _transposer = cam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        _zoom = cam.GetComponent<CinemachineFollowZoom>();


        _transposer.m_XAxis.m_InputAxisName = "";
        _transposer.m_XAxis.m_InputAxisValue = 0;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _transposer.m_XAxis.m_InputAxisName = "Mouse X";
        }
        else if (Input.GetMouseButtonUp(1))
        {
            _transposer.m_XAxis.m_InputAxisName = "";
            _transposer.m_XAxis.m_InputAxisValue = 0;
        }

        var scroll = Input.mouseScrollDelta;
        if (Math.Abs(scroll.y) > 0)
        {
            _zoom.m_Width += scroll.y * zoomSpeed;
            _zoom.m_Width = Mathf.Clamp(_zoom.m_Width, 0, 1);
        }
    }
}
