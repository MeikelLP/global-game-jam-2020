using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UserFeedback : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] private int displayTime;
    
    public static UserFeedback Instance { get; private set; }

    private static IEnumerator showTimer;

    private void Start()
    {
        if (Instance != null) Destroy(this);
        Instance = this;
    }

    public void ShowInfoMessage(string message)
    {
        if (showTimer != null)
        {
            StopCoroutine(showTimer);
            textField.text = string.Empty;
            textField.gameObject.SetActive(false);
        }

        textField.text = message;
        showTimer = Timer();
        StartCoroutine(showTimer);
    }

    private IEnumerator Timer()
    {
        textField.gameObject.SetActive(true);
        yield return new WaitForSeconds(displayTime);
        textField.text = string.Empty;
        textField.gameObject.SetActive(false);
    }
}