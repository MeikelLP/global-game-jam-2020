using System;
using UnityEngine;

public class MouseCapture : MonoBehaviour
{
    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    private void OnDisable()
    {
        if (Application.isPlaying)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
