using System.Linq;
using Boo.Lang;
using UnityEngine;
using UnityEngine.Serialization;

public class PhonePart : MonoBehaviour
{

    [FormerlySerializedAs("blockedBy")] public PhonePart[] dependsOn = new PhonePart[0];
    public string title;
    public bool broken;

    public float price;
    public Vector3 OriginalLocalPosition { get; set; }
    public Quaternion OriginalLocalRotation { get; set; }

    public bool Disassemblable => dependsOn.All(blockingPart => !blockingPart.Assembled);

    public bool Assemblable => !Phone.GetDependents(this).Any(x => x.Assembled);

    public bool Assembled { get; set; }
    public Phone Phone { get; set; }

    private void Start()
    {
        var t = transform;
        OriginalLocalPosition = t.localPosition;
        OriginalLocalRotation = t.localRotation;

        Assembled = true;
    }
}
