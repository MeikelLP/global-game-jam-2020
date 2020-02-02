using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    public static Toolbar Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI toolModeText;
    [HideInInspector] public ToolMode toolMode;
    [SerializeField] private KeyCode key = KeyCode.S;

    private void Start()
    {
        if (Instance != null) Destroy(this);
        Instance = this;

        if(!toolModeText) throw new ArgumentNullException(nameof(toolModeText));

        toolMode = ToolMode.Disassemble;
        toolModeText.text = key.ToString();
    }

    private void SwapState()
    {
        switch (toolMode)
        {
            case ToolMode.Assemble:
                toolMode = ToolMode.Disassemble;
                break;
            case ToolMode.Disassemble:
                toolMode = ToolMode.Repair;
                break;
            case ToolMode.Repair:
                toolMode = ToolMode.Assemble;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            SwapState();
        }
    }
}
