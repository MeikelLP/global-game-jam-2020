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
    public bool IsBroken => parts.Any(x => x.broken);

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
        var phone = (Phone) target;
        if (GUILayout.Button("Break Phone"))
        {
            if (phone.IsBroken) return;
            var part = phone.parts[Random.Range(0, phone.parts.Length)];
            part.broken = true;
        }

        if (GUILayout.Button("Repair Phone"))
        {
            if (!phone.IsBroken) return;
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
