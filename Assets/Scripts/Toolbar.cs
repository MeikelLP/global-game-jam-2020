using System;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public static Toolbar Instance { get; private set; }

    [SerializeField] private Tool[] tools;
    [SerializeField] private TextMeshProUGUI toolModeText;
    [HideInInspector] public ToolMode toolMode;
    [SerializeField] private KeyCode key = KeyCode.S;

    private void Start()
    {
        if (Instance != null) Destroy(this);
        Instance = this;

        if (!toolModeText) throw new ArgumentNullException(nameof(toolModeText));

        toolMode = ToolMode.Disassemble;
        toolModeText.text = key.ToString();
    }

    private void SwapState()
    {
        toolMode = toolMode == ToolMode.Assemble ? ToolMode.Disassemble : ToolMode.Assemble;
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
