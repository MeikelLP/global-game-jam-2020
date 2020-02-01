using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Transform _playerTransform;

    private GameObject _screen;

    private Boolean _isPlayerInBounds;

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = gameObject.transform;
        _screen = GameObject.Find("Background");
        _isPlayerInBounds = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        if (_isPlayerInBounds)
        {
            Vector3 currentPos = _playerTransform.position;
            if (Input.GetAxis("Vertical") > 0)
            {
                _playerTransform.position = currentPos + Vector3.up * 0.1f;
            }
            else if(Input.GetAxis("Horizontal") < 0)
            {
                _playerTransform.position = currentPos + Vector3.left * 0.1f;
            }else if (Input.GetAxis("Vertical") < 0)
            {
                _playerTransform.position = currentPos + Vector3.down * 0.1f;
            }else if (Input.GetAxis("Horizontal") > 0)
            {
                _playerTransform.position = currentPos + Vector3.right * 0.1f;
            }   
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Background"))
        {
            _isPlayerInBounds = false;
        }
        Debug.Log("Exit!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Background"))
        {
            _isPlayerInBounds = true;
        }
        Debug.Log("Enter!");
    }
}
