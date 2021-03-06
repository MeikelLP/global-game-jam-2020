using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Toolbar : MonoBehaviour
{
    public static Toolbar Instance { get; private set; }

    [SerializeField] private Tool[] tools;
    [SerializeField] private TextMeshProUGUI toolModeText;
    [SerializeField] private KeyCode key = KeyCode.S;
    [SerializeField] private Transform toolInfoContainer;

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

        for (var i = 0; i < toolInfoContainer.childCount; i++)
        {
            var index = i;
            toolInfoContainer.GetChild(i).GetComponent<Button>().onClick.AddListener(() =>
            {
                while (activeTool != tools[index])
                {
                    NextTool();
                }
            });
        }

        NextTool();
    }

    private void NextTool()
    {
        var index = Array.FindIndex(tools, x => x == activeTool);
        if (index >= 0) toolInfoContainer.GetChild(index).GetComponentInChildren<TextMeshProUGUI>().color = Color.white;
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

        toolInfoContainer.GetChild(index).GetComponentInChildren<TextMeshProUGUI>().color = Color.yellow;

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
