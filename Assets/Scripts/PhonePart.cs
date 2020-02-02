using System.Linq;
using UnityEngine;

public class PhonePart : MonoBehaviour
{
    public PhonePart[] dependsOn = new PhonePart[0];
    public string title;
    public bool broken;

    public Vector3 OriginalLocalPosition { get; set; }
    public Quaternion OriginalLocalRotation { get; set; }
    public Vector3 OriginalLocalScale { get; set; }

    public bool Disassemblable => dependsOn.All(blockingPart => !blockingPart.Assembled);

    public bool Assemblable => Phone.GetDependents(this).All(x => x.Assembled);

    public bool Assembled { get; set; }
    public Phone Phone { get; set; }

    private void Start()
    {
        var t = transform;
        OriginalLocalPosition = t.localPosition;
        OriginalLocalRotation = t.localRotation;
        OriginalLocalScale = t.localScale;

        Assembled = true;
    }

    private void Reset()
    {
        title = gameObject.name;
    }

    public override string ToString()
    {
        return $"{Phone.title}/{title}";
    }
}
