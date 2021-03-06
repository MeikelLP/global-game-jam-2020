using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

#endif

public class Phone : MonoBehaviour
{
    public PhonePart[] parts;
    public string title;

    private int _completeNumberOfParts;
    
    public IEnumerable<PhonePart> GetDependents(PhonePart part)
    {
        return parts.Where(x => x.dependsOn.Contains(part));
    }

    public void Initialize()
    {
        _completeNumberOfParts = parts.Length;
        foreach (var part in parts)
        {
            part.Initialize(this);
        }
    }

    public void RemovePart(PhonePart toRemove)
    {
        var currentNumberOfParts = parts.Length;
        var tmp = parts.ToList();
        tmp.Remove(toRemove);
        parts = tmp.ToArray();
        // for (int i = 0; i < currentNumberOfParts; i++)
        // {
        //     if (parts[i] == toRemove)
        //     {
        //         // switch the last element with the current
        //         parts[i] = parts[currentNumberOfParts - 1];
        //         return;
        //     }
        // }
    }
    
    public void AddPart(PhonePart toAdd)
    {
        // TODO check that one unique part can only be added once
        // TODO check that the correct amount of screws is assembled
        var tmp = parts.ToList();
        tmp.Add(toAdd);
        parts = tmp.ToArray();
    }

    /// <summary>
    /// The phone must be complete and no part can be broken 
    /// </summary>
    /// <returns></returns>
    public bool IsRepaired()
    {
        return _completeNumberOfParts == parts.Length && !AnyBroken();
    }
    
    public bool AnyBroken()
    {
        return parts.Any(phonePart => phonePart.broken);
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(Phone))]
public class PhoneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var phone = (Phone) target;
        if (GUILayout.Button("Break Phone"))
        {
            if (phone.AnyBroken()) return;
            var part = phone.parts[Random.Range(0, phone.parts.Length)];
            part.broken = true;
        }

        if (GUILayout.Button("Repair Phone"))
        {
            if (!phone.AnyBroken()) return;
            foreach (var part in phone.parts)
            {
                part.broken = false;
            }
        }

        if (GUILayout.Button("Dump dependency tree to Clipboard"))
        {
            var topLevel = phone.parts.Where(x => x.dependsOn == null || x.dependsOn.Length == 0).ToArray();
            var sb = new StringBuilder();
            foreach (var tlPart in topLevel)
            {
                AddDependentsLine(sb, phone, tlPart, 0);
            }

            EditorGUIUtility.systemCopyBuffer = sb.ToString();
        }
    }

    private static void AddDependentsLine(StringBuilder sb, Phone phone, PhonePart part, int level)
    {
        var offset = new string(Enumerable.Range(0, level).Select(x => ' ').ToArray());
        sb.AppendLine($"{offset}{part.title}");
        foreach (var dependent in phone.GetDependents(part).ToArray())
        {
            AddDependentsLine(sb, phone, dependent, level + 1);
        }
    }
}
#endif
