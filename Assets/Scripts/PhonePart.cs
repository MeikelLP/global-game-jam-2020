using System.Linq;
using Boo.Lang;
using UnityEngine;

public class PhonePart : MonoBehaviour
{
    public Vector3 OriginalLocalPosition { get; set; }
    public Quaternion OriginalLocalRotation { get; set; }

    public bool Assembled { get; set; }

    public PhonePart[] blockedBy = new PhonePart[0];
    public PhonePart[] dependentOf = new PhonePart[0];
    public string title;
    
    private void Start()
    {
        var t = transform;
        OriginalLocalPosition = t.localPosition;
        OriginalLocalRotation = t.localRotation;

        Assembled = true;
    }

    public bool Disassemblable()
    {
        return blockedBy.All(blockingPart => !blockingPart.Assembled);
    }
    
    public bool Assemblable()
    {
        return dependentOf.All(dependingPart => dependingPart.Assembled);
    }
}
