using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarBehaviour : MonoBehaviour
{
    public enum ETool{
        DISAESSEMBLY = 1,
        ASSEMBLY = 2,
        DUMMY = 3
    }
    
    private Text _text;
    
    [HideInInspector] public ETool tool;


    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponentInChildren<Text>();
        tool = ETool.DISAESSEMBLY;
        ChangeUI(tool);
    }

    private void SwapState()
    {
        tool = (ETool) (tool + 1);
        if (tool == ETool.DUMMY)
        {
            tool = (ETool) 1;
        }
        ChangeUI(tool);
    }
    
    private void ChangeUI(ETool eTool)
    {
        _text.text = "Tool: " + eTool.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("s"))
        {
            SwapState();
        }
    }
}
