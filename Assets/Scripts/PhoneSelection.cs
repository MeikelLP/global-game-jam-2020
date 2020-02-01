using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class PhoneSelection : MonoBehaviour
{
    public static PhoneSelection Instance { get; private set; }

    public Transform phone;
    [SerializeField] private Text toolModeText;
    [HideInInspector] public ToolMode toolMode;
    [SerializeField] private KeyCode key = KeyCode.S;

    private void Start()
    {
        if (Instance != null) Destroy(this);
        Instance = this;

        if(!phone) throw new ArgumentNullException(nameof(phone));
        if(!toolModeText) throw new ArgumentNullException(nameof(toolModeText));

        toolMode = ToolMode.Disassemble;
        ChangeUI();
    }

    private void SwapState()
    {
        toolMode = toolMode == ToolMode.Assemble ? ToolMode.Disassemble : ToolMode.Assemble;
        ChangeUI();
    }

    private void ChangeUI()
    {
        toolModeText.text = $"Tool: {toolMode} (<color=yellow>{key}</color>)";
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
