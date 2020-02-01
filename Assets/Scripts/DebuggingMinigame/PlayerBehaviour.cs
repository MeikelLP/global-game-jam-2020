using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    private Transform _playerTransform;

    private GameObject _screen;

    private Boolean _isPlayerInBounds;

    private Rigidbody2D _rigid;

    public float speed = 0.1f;

    public Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        _playerTransform = gameObject.transform;
        _screen = GameObject.Find("Background");
        _isPlayerInBounds = true;
        _rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        Debug.Log(_playerTransform.position);
    }

    private void UpdatePosition()
    {
        Vector3 currentPos = _playerTransform.position;
        if (Input.GetAxis("Vertical") > 0)
        { 
            TryMove(currentPos,Vector3.up * speed);
        }
        else if(Input.GetAxis("Horizontal") < 0)
        {
            TryMove(currentPos,Vector3.left * speed);
        }else if (Input.GetAxis("Vertical") < 0)
        {
            TryMove(currentPos,Vector3.down * speed);
        }else if (Input.GetAxis("Horizontal") > 0)
        {
            TryMove(currentPos,Vector3.right * speed);
        }

       //Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(Vector3.zero);
       //Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }

    private void TryMove(Vector3 oldPos, Vector3 add)
    {
        Vector3 targetPos = oldPos + add;
        if (IsPlayerInBoundsTest(targetPos))
        {
            _playerTransform.position = targetPos;
        }
        else
        {
            _playerTransform.position = oldPos;
        }
    }

    private bool IsPlayerInBoundsTest(Vector3 targetPos)
    {
        
        return targetPos.x < mainCamera.scaledPixelWidth &&
               targetPos.y < mainCamera.scaledPixelHeight;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Background"))
        {
            _isPlayerInBounds = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Background"))
        {
            _isPlayerInBounds = true;
        }
    }
}
