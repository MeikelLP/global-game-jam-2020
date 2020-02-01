using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineVirtualCamera))]
public class CinemachineMouseDown : MonoBehaviour
{
    private CinemachineOrbitalTransposer _transposer;

    private void Start()
    {
        var cam = GetComponent<CinemachineVirtualCamera>();
        _transposer = cam.GetCinemachineComponent<CinemachineOrbitalTransposer>();


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
    }
}
