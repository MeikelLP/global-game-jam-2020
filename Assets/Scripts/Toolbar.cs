using System;
using TMPro;
using UnityEngine;

public class Toolbar : MonoBehaviour
{
    public static Toolbar Instance { get; private set; }

    [SerializeField] private Tool[] tools;
    [SerializeField] private TextMeshProUGUI toolModeText;
    [SerializeField] private KeyCode key = KeyCode.S;

    public Tool activeTool;

    private void Start()
    {
        if (Instance != null) Destroy(this);
        Instance = this;

        if (!toolModeText) throw new ArgumentNullException(nameof(toolModeText));

        foreach (var tool in tools)
        {
            tool.enabled = false;
            tool.hoverIcon.enabled = false;
        }

        toolModeText.text = key.ToString();
        NextTool();
    }

    private void NextTool()
    {
        var index = Array.FindIndex(tools, x => x == activeTool);
        index++;
        if (index >= tools.Length)
        {
            index = 0;
        }

        if (activeTool)
        {
            activeTool.hoverIcon.enabled = false;
            activeTool.enabled = false;
        }

        activeTool = tools[index];
        activeTool.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            NextTool();
        }
    }
}
