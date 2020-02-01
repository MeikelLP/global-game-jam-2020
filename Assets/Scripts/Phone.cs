using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Phone : MonoBehaviour
{
    public PhonePart[] parts;
    public bool IsBroken => parts.Any(x => x.broken);


    public void BreakOnePart()
    {
        if (IsBroken) return;
        var part = parts[Random.Range(0, parts.Length)];
        part.broken = true;
    }

    public IEnumerable<PhonePart> GetDependents(PhonePart part)
    {
        return parts.Where(x => x.dependsOn.Contains(part));
    }

    private void Start()
    {
        foreach (var part in parts)
        {
            part.Phone = this;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Phone))]
public class PhoneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var phone = ((Phone) target);
        if (GUILayout.Button("Break Phone"))
        {
            phone.BreakOnePart();
        }

        if (GUILayout.Button("Dump dependency tree to Clipboard"))
        {
            // EditorGUIUtility.systemCopyBuffer =
        }
    }
}
#endif
