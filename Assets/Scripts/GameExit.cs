using TMPro;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    [SerializeField] private GameObject confirmDialog;
    [SerializeField] private KeyCode key = KeyCode.Escape;
    [SerializeField] private TextMeshProUGUI text;
    private bool _confirmed;

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            Application.wantsToQuit += ApplicationOnwantsToQuit;
        }
    }

    private void OnDisable()
    {
        if (Application.isPlaying)
        {
            Application.wantsToQuit -= ApplicationOnwantsToQuit;
        }
    }

    private void Start()
    {
        text.text = "ESC"; //key.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(key))
        {
            TryExitGame();
        }
    }

    private bool ApplicationOnwantsToQuit()
    {
        if (_confirmed) return true;

        ConfirmExit();
        return false;
    }

    public void TryExitGame()
    {
        ConfirmExit();
    }

    private void ConfirmExit()
    {
        confirmDialog.SetActive(true);
    }

    public void ConfirmedExit()
    {
        _confirmed = true;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void CancelExit()
    {
        _confirmed = false;
        confirmDialog.SetActive(false);
    }
}
