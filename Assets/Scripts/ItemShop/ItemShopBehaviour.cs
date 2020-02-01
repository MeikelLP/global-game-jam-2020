using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemShopBehaviour : MonoBehaviour
{
     private Text _keyInfoText;
    
        public KeyCode _open;
    
        private GameObject _scrollView;
        
        // Start is called before the first frame update
        void Start()
        {
            _keyInfoText = GetComponentInChildren<Text>();
            _keyInfoText.text = "[" + _open.ToString() + "] Shop";
            _scrollView = GameObject.Find("Shop");
            _scrollView.SetActive(false);
            ScanForComponents();
        }
    
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(_open) )
            {
                if (_scrollView.activeSelf)
                {
                    _scrollView.SetActive(false);
                }
                else
                {
                    _scrollView.SetActive(true);
                }
            }
        }
    
        private void ScanForComponents()
        {
            
        }
}
