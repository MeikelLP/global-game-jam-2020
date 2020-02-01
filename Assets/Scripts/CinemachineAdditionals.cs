using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class CinemachineAdditionals : MonoBehaviour
{
    [SerializeField] private float zoomSpeed = 0.1f;
    [SerializeField] private KeyCode rotateKey = KeyCode.Mouse1;
    [SerializeField] private string zoomKey = "MouseScroll";

    [SerializeField] private CinemachineFollowZoom zoom;
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private TextMeshProUGUI rotateText;
    [SerializeField] private TextMeshProUGUI zoomText;

    private CinemachineOrbitalTransposer _transposer;

    private void Start()
    {
        if (!rotateText) throw new ArgumentNullException(nameof(rotateText));
        if (!zoomText) throw new ArgumentNullException(nameof(rotateText));
        if (!zoom) throw new ArgumentNullException(nameof(rotateText));

        _transposer = cam.GetCinemachineComponent<CinemachineOrbitalTransposer>();
        zoom = cam.GetComponent<CinemachineFollowZoom>();


        _transposer.m_XAxis.m_InputAxisName = "";
        _transposer.m_XAxis.m_InputAxisValue = 0;

        rotateText.text = $"Rotate (<color=yellow>{rotateKey}</color>)";
        zoomText.text = $"Zoom (<color=yellow>{zoomKey}</color>)";
    }

    private void Update()
    {
        if (Input.GetKeyDown(rotateKey))
        {
            _transposer.m_XAxis.m_InputAxisName = "Mouse X";
        }
        else if (Input.GetKeyUp(rotateKey))
        {
            _transposer.m_XAxis.m_InputAxisName = "";
            _transposer.m_XAxis.m_InputAxisValue = 0;
        }

        var scroll = Input.mouseScrollDelta;
        if (Math.Abs(scroll.y) > 0)
        {
            zoom.m_Width -= scroll.y * zoomSpeed;
            zoom.m_Width = Mathf.Clamp(zoom.m_Width, 0, 1);
        }
    }
}
